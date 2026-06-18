using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // source PDF
        const string outputRoot = "Attachments";      // root folder for extracted files

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the root output directory exists
        Directory.CreateDirectory(outputRoot);

        try
        {
            // Load the PDF document (lifecycle rule: use using for deterministic disposal)
            using (Document doc = new Document(inputPdf))
            {
                // In recent Aspose.Pdf versions attachments are exposed via the EmbeddedFiles collection
                if (doc.EmbeddedFiles == null || doc.EmbeddedFiles.Count == 0)
                {
                    Console.WriteLine("No attachments found in the PDF.");
                    return;
                }

                int index = 1;
                // Iterate over each embedded file (attachment) in the document using reflection –
                // this avoids a direct compile‑time dependency on the EmbeddedFile type which may
                // reside in a version‑specific namespace.
                foreach (var embedded in doc.EmbeddedFiles)
                {
                    // ----- Resolve attachment name -----
                    var nameProp = embedded.GetType().GetProperty("Name");
                    string attachmentName = nameProp?.GetValue(embedded) as string;
                    if (string.IsNullOrWhiteSpace(attachmentName))
                        attachmentName = $"Attachment_{index}";

                    // Create a subfolder for this attachment (one folder per attachment)
                    string baseName = Path.GetFileNameWithoutExtension(attachmentName);
                    if (string.IsNullOrWhiteSpace(baseName))
                        baseName = $"Attachment_{index}";
                    string subFolder = Path.Combine(outputRoot, baseName);
                    Directory.CreateDirectory(subFolder);

                    // ----- Resolve attachment data -----
                    string outPath = Path.Combine(subFolder, attachmentName);
                    // Prefer a Data stream if the property exists
                    var dataProp = embedded.GetType().GetProperty("Data");
                    var dataStream = dataProp?.GetValue(embedded) as Stream;
                    if (dataStream != null)
                    {
                        using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                        {
                            dataStream.CopyTo(fs);
                        }
                    }
                    else
                    {
                        // Fallback – some versions expose a Save(string) method
                        var saveMethod = embedded.GetType().GetMethod("Save", new[] { typeof(string) });
                        if (saveMethod != null)
                        {
                            saveMethod.Invoke(embedded, new object[] { outPath });
                        }
                        else
                        {
                            Console.Error.WriteLine($"Unable to extract attachment: {attachmentName}");
                            index++;
                            continue;
                        }
                    }

                    Console.WriteLine($"Extracted: {attachmentName} -> {outPath}");
                    index++;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
