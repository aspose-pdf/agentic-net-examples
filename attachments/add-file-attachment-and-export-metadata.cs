using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath = "input.pdf";
        const string attachmentPath = "attachment.txt";
        const string outputPdfPath = "output_with_attachment.pdf";
        const string metadataCsvPath = "attachment_metadata.csv";

        // Validate input files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Open the PDF, add the attachment, and save
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a FileSpecification for the attachment
            // The first argument is the file to embed, the second is a description (optional)
            FileSpecification fileSpec = new FileSpecification(attachmentPath, Path.GetFileName(attachmentPath));
            // Optionally set modification date via Params (CreationDate is also available if needed)
            if (fileSpec.Params != null)
            {
                fileSpec.Params.ModDate = DateTime.UtcNow;
            }
            pdfDoc.EmbeddedFiles.Add(fileSpec);

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        // Re-open the saved PDF to read attachment metadata
        using (Document pdfDoc = new Document(outputPdfPath))
        {
            // Prepare CSV writer
            using (StreamWriter csvWriter = new StreamWriter(metadataCsvPath, false))
            {
                // Write CSV header
                csvWriter.WriteLine("Name,Description,CreationDate,ModificationDate,Size");

                // Iterate over all embedded files
                foreach (FileSpecification fileSpec in pdfDoc.EmbeddedFiles)
                {
                    // Extract metadata, handling possible nulls
                    string name = fileSpec.Name ?? string.Empty;
                    string description = fileSpec.Description ?? string.Empty;

                    var paramsObj = fileSpec.Params;
                    string creationDate = string.Empty;
                    string modificationDate = string.Empty;
                    string size = string.Empty;

                    if (paramsObj != null)
                    {
                        // CreationDate and ModDate are value types (DateTime). Use MinValue check.
                        if (paramsObj.CreationDate != DateTime.MinValue)
                            creationDate = paramsObj.CreationDate.ToString("o"); // ISO 8601

                        if (paramsObj.ModDate != DateTime.MinValue)
                            modificationDate = paramsObj.ModDate.ToString("o");

                        // Size is an int value type.
                        size = paramsObj.Size.ToString();
                    }

                    // Escape commas in text fields
                    name = EscapeCsv(name);
                    description = EscapeCsv(description);

                    // Write CSV line
                    csvWriter.WriteLine($"{name},{description},{creationDate},{modificationDate},{size}");
                }
            }
        }

        Console.WriteLine($"Attachment added and metadata exported to '{metadataCsvPath}'.");
    }

    // Helper to escape CSV fields containing commas or quotes
    static string EscapeCsv(string field)
    {
        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
        {
            field = field.Replace("\"", "\"\"");
            return $"\"{field}\"";
        }
        return field;
    }
}
