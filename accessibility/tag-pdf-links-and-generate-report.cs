using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "tagged_output.pdf";
        const string reportPath = "link_verification_report.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Access the tagged content API
                ITaggedContent taggedContent = doc.TaggedContent;

                // Set document language and title (optional)
                taggedContent.SetLanguage("en-US");
                taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

                // Get the root of the logical structure tree
                StructureElement root = taggedContent.RootElement;

                // Find all link elements in the structure (recursive search)
                var linkElements = root.FindElements<LinkElement>(true);

                // Prepare a report writer
                using (StreamWriter reportWriter = new StreamWriter(reportPath, false))
                {
                    reportWriter.WriteLine($"Link Elements Verification Report - {DateTime.Now}");
                    reportWriter.WriteLine(new string('=', 50));
                    reportWriter.WriteLine();

                    int index = 1;
                    foreach (LinkElement link in linkElements)
                    {
                        // Example tagging: set alternative text if missing
                        if (string.IsNullOrEmpty(link.AlternativeText))
                        {
                            link.AlternativeText = $"Link #{index}";
                        }

                        // Write verification info to the report
                        reportWriter.WriteLine($"Link #{index}");
                        reportWriter.WriteLine($"  ActualText      : {link.ActualText ?? "(none)"}");
                        reportWriter.WriteLine($"  AlternativeText : {link.AlternativeText}");
                        reportWriter.WriteLine($"  Language        : {link.Language ?? "(inherit)"}");
                        reportWriter.WriteLine();

                        index++;
                    }

                    reportWriter.WriteLine($"Total link elements processed: {linkElements.Count}");
                }

                // Save the modified PDF (no PreSave call required)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
            Console.WriteLine($"Verification report written to '{reportPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}