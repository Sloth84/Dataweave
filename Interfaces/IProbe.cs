using System.Text.Json.Serialization;

namespace Dataweave.Interfaces
{
    public interface IProbe
    {
        public class MetaData
        {
            public Streams Streams { get; set; } = new();

            public List<IChapters.Chapter> Chapters { get; set; } = [];

            public IFormat.Format? Format { get; set; }
        }

        public class Streams
        {
            public List<IStreams.Video> Video { get; set; } = [];
            public List<IStreams.Audio> Audio { get; set; } = [];
            public List<IStreams.Subtitle> Subtitle { get; set; } = [];
        }

        public interface IStreams
        {
            record Video(
                [property: JsonPropertyName("index")] int? Index,
                [property: JsonPropertyName("codec_name")] string? CodecName,
                [property: JsonPropertyName("codec_long_name")] string? CodecLongName,
                [property: JsonPropertyName("profile")] string? Profile,
                [property: JsonPropertyName("codec_type")] string? CodecType,
                [property: JsonPropertyName("codec_tag_string?")] string? CodecTagstring,
                [property: JsonPropertyName("codec_tag")] string? CodecTag,
                [property: JsonPropertyName("width")] int? Width,
                [property: JsonPropertyName("height")] int? Height,
                [property: JsonPropertyName("coded_width")] int? CodedWidth,
                [property: JsonPropertyName("coded_height")] int? CodedHeight,
                [property: JsonPropertyName("closed_captions")] int? ClosedCaptions,
                [property: JsonPropertyName("film_grain")] int? FilmGrain,
                [property: JsonPropertyName("has_b_frames")] int? HasBFrames,
                [property: JsonPropertyName("sample_aspect_ratio")] string? SampleAspectRatio,
                [property: JsonPropertyName("display_aspect_ratio")] string? DisplayAspectRatio,
                [property: JsonPropertyName("pix_fmt")] string? PixFmt,
                [property: JsonPropertyName("level")] int? Level,
                [property: JsonPropertyName("chroma_location")] string? ChromaLocation,
                [property: JsonPropertyName("field_order")] string? FieldOrder,
                [property: JsonPropertyName("refs")] int? Refs,
                [property: JsonPropertyName("is_avc")] string? IsAvc,
                [property: JsonPropertyName("nal_length_size")] string? NalLengthSize,
                [property: JsonPropertyName("r_frame_rate")] string? RFrameRate,
                [property: JsonPropertyName("avg_frame_rate")] string? AvgFrameRate,
                [property: JsonPropertyName("time_base")] string? TimeBase,
                [property: JsonPropertyName("start_pts")] int? StartPts,
                [property: JsonPropertyName("start_time")] string? StartTime,
                [property: JsonPropertyName("bits_per_raw_sample")] string? BitsPerRawSample,
                [property: JsonPropertyName("extradata_size")] int? ExtradataSize,
                [property: JsonPropertyName("disposition")] Disposition? Disposition,
                [property: JsonPropertyName("tags")] Tags? Tags
             );

            record Audio(
                [property: JsonPropertyName("index")] int? Index,
                [property: JsonPropertyName("codec_name")] string? CodecName,
                [property: JsonPropertyName("codec_long_name")] string? CodecLongName,
                [property: JsonPropertyName("codec_type")] string? CodecType,
                [property: JsonPropertyName("codec_tag_string?")] string? CodecTagstring,
                [property: JsonPropertyName("codec_tag")] string? CodecTag,
                [property: JsonPropertyName("sample_fmt")] string? SampleFmt,
                [property: JsonPropertyName("sample_rate")] string? SampleRate,
                [property: JsonPropertyName("channels")] int? Channels,
                [property: JsonPropertyName("channel_layout")] string? ChannelLayout,
                [property: JsonPropertyName("bits_per_sample")] int? BitsPerSample,
                [property: JsonPropertyName("initial_padding")] int? InitialPadding,
                [property: JsonPropertyName("r_frame_rate")] string? RFrameRate,
                [property: JsonPropertyName("avg_frame_rate")] string? AvgFrameRate,
                [property: JsonPropertyName("time_base")] string? TimeBase,
                [property: JsonPropertyName("start_pts")] int? StartPts,
                [property: JsonPropertyName("start_time")] string? StartTime,
                [property: JsonPropertyName("bit_rate")] string? BitRate,
                [property: JsonPropertyName("disposition")] Disposition? Disposition,
                [property: JsonPropertyName("tags")] Tags? Tags
            );

            record Subtitle(
                [property: JsonPropertyName("index")] int? Index,
                [property: JsonPropertyName("codec_name")] string? CodecName,
                [property: JsonPropertyName("codec_long_name")] string? CodecLongName,
                [property: JsonPropertyName("codec_type")] string? CodecType,
                [property: JsonPropertyName("codec_tag_string?")] string? CodecTagstring,
                [property: JsonPropertyName("codec_tag")] string? CodecTag,
                [property: JsonPropertyName("width")] int? Width,
                [property: JsonPropertyName("height")] int? Height,
                [property: JsonPropertyName("r_frame_rate")] string? RFrameRate,
                [property: JsonPropertyName("avg_frame_rate")] string? AvgFrameRate,
                [property: JsonPropertyName("time_base")] string? TimeBase,
                [property: JsonPropertyName("start_pts")] int? StartPts,
                [property: JsonPropertyName("start_time")] string? StartTime,
                [property: JsonPropertyName("disposition")] Disposition? Disposition,
                [property: JsonPropertyName("tags")] Tags? Tags
            );

            record Disposition(
                [property: JsonPropertyName("default")] int? Default,
                [property: JsonPropertyName("dub")] int? Dub,
                [property: JsonPropertyName("original")] int? Original,
                [property: JsonPropertyName("comment")] int? Comment,
                [property: JsonPropertyName("lyrics")] int? Lyrics,
                [property: JsonPropertyName("karaoke")] int? Karaoke,
                [property: JsonPropertyName("forced")] int? Forced,
                [property: JsonPropertyName("hearing_impaired")] int? HearingImpaired,
                [property: JsonPropertyName("visual_impaired")] int? VisualImpaired,
                [property: JsonPropertyName("clean_effects")] int? CleanEffects,
                [property: JsonPropertyName("attached_pic")] int? AttachedPic,
                [property: JsonPropertyName("timed_thumbnails")] int? TimedThumbnails,
                [property: JsonPropertyName("non_diegetic")] int? NonDiegetic,
                [property: JsonPropertyName("captions")] int? Captions,
                [property: JsonPropertyName("descriptions")] int? Descriptions,
                [property: JsonPropertyName("metadata")] int? Metadata,
                [property: JsonPropertyName("dependent")] int? Dependent,
                [property: JsonPropertyName("still_image")] int? StillImage
            );

            record Tags(
                [property: JsonPropertyName("language")] string? Language
            );
        }

        public interface IFormat
        {
            record Format(
                [property: JsonPropertyName("filename")] string? Filename,
                [property: JsonPropertyName("nb_streams")] int? NbStreams,
                [property: JsonPropertyName("nb_programs")] int? NbPrograms,
                [property: JsonPropertyName("nb_stream_groups")] int? NbStreamGroups,
                [property: JsonPropertyName("format_name")] string? FormatName,
                [property: JsonPropertyName("format_long_name")] string? FormatLongName,
                [property: JsonPropertyName("start_time")] string? StartTime,
                [property: JsonPropertyName("duration")] string? Duration,
                [property: JsonPropertyName("size")] string? Size,
                [property: JsonPropertyName("bit_rate")] string? BitRate,
                [property: JsonPropertyName("probe_score")] int? ProbeScore,
                [property: JsonPropertyName("tags")] Tags? Tags
            );

            record Tags(
                [property: JsonPropertyName("title")] string? Title,
                [property: JsonPropertyName("encoder")] string? Encoder,
                [property: JsonPropertyName("creation_time")] DateTime? CreationTime
            );
        }

        public interface IChapters
        {
            record Chapter(
                [property: JsonPropertyName("id")] float? Id,
                [property: JsonPropertyName("time_base")] string? TimeBase,
                [property: JsonPropertyName("start")] float? Start,
                [property: JsonPropertyName("start_time")] string? StartTime,
                [property: JsonPropertyName("end")] float? End,
                [property: JsonPropertyName("end_time")] string? EndTime,
                [property: JsonPropertyName("tags")] Tags? Tags
            );

            record Tags(
                [property: JsonPropertyName("title")] string? Title
            );
        }
    }
}