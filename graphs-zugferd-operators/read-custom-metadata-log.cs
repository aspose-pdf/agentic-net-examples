using System;
using System.IO;
using Aspose.Pdf;

namespace ReadCustomMetadata
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create sample PDF with custom metadata
            using (Document doc = new Document())
            {
                // Add a blank page (required to have a page)
                doc.Pages.Add();

                // Add custom metadata entries (max 4)
                doc.Info.Add("ProjectId", "12345");
                doc.Info.Add("Reviewer", "John Doe");
                doc.Info.Add("ComplianceLevel", "Level1");

                // Save the PDF
                doc.Save("input.pdf");
            }

            // Reopen the PDF and read custom metadata
            using (Document doc = new Document("input.pdf"))
            {
                // Open log file for writing
                using (StreamWriter writer = new StreamWriter("audit.log"))
                {
                    foreach (KeyValuePair<string, string> entry in doc.Info)
                    {
                        // Skip predefined keys
                        if (DocumentInfo.IsPredefinedKey(entry.Key))
                        {
                            continue;
                        }

                        writer.WriteLine(entry.Key + ": " + entry.Value);
                    }
                }
            }
        }
    }
}
