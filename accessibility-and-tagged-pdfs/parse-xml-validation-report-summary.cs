using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

class Program
{
    // Entry point
    static void Main()
    {
        // Path to the XML validation report – adjust as needed
        const string reportPath = "validation_report.xml";

        if (!File.Exists(reportPath))
        {
            Console.Error.WriteLine($"Report file not found: {reportPath}");
            return;
        }

        try
        {
            // Load the XML document
            XDocument doc = XDocument.Load(reportPath);

            // Expected structure:
            // <Report>
            //   <Error>
            //     <Code>ERR001</Code>
            //     <Message>Some description</Message>
            //   </Error>
            //   ...
            // </Report>

            // Extract all error elements
            var errorElements = doc.Descendants("Error");

            // Dictionary to count occurrences of each error code
            var errorCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            // Dictionary to collect distinct messages per code
            var errorMessages = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);

            foreach (var err in errorElements)
            {
                // Use the safe navigation operator to avoid null‑reference warnings (CS8600)
                string code = err.Element("Code")?.Value ?? "UNKNOWN";
                string message = err.Element("Message")?.Value ?? string.Empty;

                // Update count
                if (errorCounts.ContainsKey(code))
                    errorCounts[code]++;
                else
                    errorCounts[code] = 1;

                // Store distinct messages
                if (!errorMessages.ContainsKey(code))
                    errorMessages[code] = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                if (!string.IsNullOrWhiteSpace(message))
                    errorMessages[code].Add(message.Trim());
            }

            // Output summary
            Console.WriteLine("=== Validation Report Summary ===");
            Console.WriteLine($"Total errors found: {errorCounts.Values.Sum()}");
            Console.WriteLine();

            foreach (var kvp in errorCounts.OrderBy(k => k.Key))
            {
                string code = kvp.Key;
                int count = kvp.Value;
                Console.WriteLine($"Error Code: {code}");
                Console.WriteLine($"  Occurrences: {count}");

                if (errorMessages.TryGetValue(code, out var msgs) && msgs.Count > 0)
                {
                    Console.WriteLine("  Sample messages:");
                    foreach (var msg in msgs.Take(3)) // show up to 3 distinct messages
                        Console.WriteLine($"    - {msg}");
                }
                else
                {
                    Console.WriteLine("  No message details available.");
                }

                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to process report: {ex.Message}");
        }
    }
}
