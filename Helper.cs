using System.Diagnostics;

namespace Dataweave
{
    public class Helper
    {
        public static string TrimStringArray(string? input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;

            string[] inputArray = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            return string.Join(" ", inputArray);
        }

        public static string ProcessOutput(ProcessStartInfo psi)
        {
            using Process process = new()
            {
                StartInfo = psi
            };

            try
            {
                process.Start();
                string results = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                return results;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error running ffprobe: {ex.Message}");
                return string.Empty;
            }
        }
    }
}