using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using static Dataweave.Interfaces.IProbe;

namespace Dataweave
{
    public class FFprobe
    {
        #region Parameters

        private int? _analyzeduration;
        private int? _probesize;

        /// <summary>
        /// Specify how many microseconds are analyzed to probe the input. A higher value will enable detecting more accurate information,
        /// but will increase latency. It defaults to 5,000,000 microseconds = 5 seconds.
        /// </summary>
        internal int? Analyzeduration
        {
            get => _analyzeduration;
            set
            {
                if (!value.HasValue || value < 0 || value > int.MaxValue)
                    _analyzeduration = 5000000;
                else
                    _analyzeduration = value;
            }
        }

        /// <summary>
        /// Set probing size in bytes, i.e. the size of the data to analyze to get stream information.
        /// A higher value will enable detecting more information in case it is dispersed into the stream, but will increase latency.
        /// Must be an integer no lesser than 32. It is 5000000 by default.
        /// </summary>
        internal int? Probesize
        {
            get => _probesize;
            set
            {
                if (!value.HasValue || value < 32 || value > int.MaxValue)
                    _probesize = 5000000;
                else
                    _probesize = value;
            }
        }

        /// <summary>
        /// If set to true, the output will be in units of 1/1000 of a second.
        /// </summary>
        internal bool? Unit { get; set; } = false;

        /// <summary>
        /// If set to true, the output will be in sexagesimal format (HH:MM:SS.milliseconds).
        /// </summary>
        internal bool? Sexagesimal { get; set; } = false;

        /// <summary>
        /// If set to true, the output will be bitexact, meaning that the output will not contain any additional information that is not present in the input file.
        /// </summary>
        internal bool? Bitexact { get; set; } = false;

        #endregion Parameters

        private MetaData GetMetadata(string args, string ffprobePath = "ffprobe")
        {
            MetaData metaData = new();

            var ffprobePSI = new ProcessStartInfo
            {
                FileName = ffprobePath,
                Arguments = args,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            string json = Helper.ProcessOutput(ffprobePSI);

            if (string.IsNullOrWhiteSpace(json) || string.IsNullOrEmpty(json)) return metaData;

            try
            {
                JsonNode? nodes = JsonNode.Parse(json);
                if (nodes == null) return metaData;

                Metadata = new();

                // Streams
                if (nodes["streams"] is JsonArray streamsArray && streamsArray.Count > 0)
                {
                    metaData.Streams.Video = streamsArray
                       .Where(stream => stream?["codec_type"]?.ToString() == "video")
                       .Select(video => video.Deserialize<IStreams.Video>())
                       .Where(v => v is not null)
                       .ToList()!;

                    metaData.Streams.Audio = streamsArray
                      .Where(stream => stream?["codec_type"]?.ToString() == "audio")
                      .Select(video => video.Deserialize<IStreams.Audio>())
                      .Where(v => v is not null)
                      .ToList()!;

                    metaData.Streams.Subtitle = streamsArray
                      .Where(stream => stream?["codec_type"]?.ToString() == "subtitle")
                      .Select(video => video.Deserialize<IStreams.Subtitle>())
                      .Where(v => v is not null)
                      .ToList()!;
                }

                // Chapters
                if (nodes["chapters"]?.AsArray().Count > 0)
                {
                    metaData.Chapters = [.. nodes["chapters"]!.AsArray().Select(chapter => chapter.Deserialize<IChapters.Chapter>()!)];
                }

                // Format
                if (nodes["format"]?.AsObject().Count > 0)
                {
                    metaData.Format = nodes["format"].Deserialize<IFormat.Format>()!;
                }

                return metaData;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error parsing JSON: {ex.Message}");
                return metaData;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                return metaData;
            }
        }

        /// <summary>
        /// Gets the metadata associated with the current instance.
        /// </summary>
        public MetaData Metadata { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FFprobe"/> class with the specified file name and optional parameters.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="ffprobePath"></param>
        /// <param name="analyzeduration"></param>
        /// <param name="probesize"></param>
        /// <param name="unit"></param>
        /// <param name="sexagesimal"></param>
        /// <param name="bitexact"></param>
        /// <exception cref="FileNotFoundException"></exception>
        public FFprobe(string fileName, string ffprobePath = "ffprobe", int? analyzeduration = null, int? probesize = null, bool? unit = false, bool? sexagesimal = false, bool? bitexact = false)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException($"File {fileName} does not exist.");

            Analyzeduration = analyzeduration;
            Probesize = probesize;
            Unit = unit;
            Sexagesimal = sexagesimal;
            Bitexact = bitexact;

            StringBuilder sb = new();
            sb.AppendLine("-hide_banner");
            sb.AppendLine("-v quiet");

            if (Analyzeduration != null)
                sb.AppendLine($"-analyzeduration {Analyzeduration}");

            if (Probesize != null)
                sb.AppendLine($"-probesize {Probesize}");

            if (Unit == true)
                sb.AppendLine("-unit");

            if (Sexagesimal == true)
                sb.AppendLine("-sexagesimal");

            sb.AppendLine("-show_streams");
            sb.AppendLine("-show_chapters");
            sb.AppendLine("-show_format");
            sb.AppendLine("-of json");
            sb.AppendLine($"\"{fileName}\"");

            Metadata = GetMetadata(Helper.TrimStringArray(sb.ToString()), ffprobePath);

            sb.Clear();
        }
    }
}