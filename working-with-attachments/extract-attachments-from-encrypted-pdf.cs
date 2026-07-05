using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Resolve the PDF path – first try the argument (if supplied), otherwise look in the executable folder.
        string encryptedPdfPath = args.Length > 0 ? args[0] : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "encrypted.pdf");
        const string password = "user123";               // password to open the PDF
        const string outputFolder = "Attachments";       // folder to store extracted files

        // Verify that the encrypted PDF actually exists before attempting to open it.
        if (!File.Exists(encryptedPdfPath))
        {
            Console.WriteLine($"Error: Could not find the encrypted PDF at '{encryptedPdfPath}'.");
            Console.WriteLine("Place the file in the specified location or pass its full path as a command‑line argument.");
            return;
        }

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputFolder);

        // Open the encrypted PDF with the supplied password. The Document constructor that accepts (string filename, string password) loads the file.
        // Decrypt() removes encryption; after this the document can be accessed normally.
        using (Document doc = new Document(encryptedPdfPath, password))
        {
            // Decrypt the document (no parameters).
            doc.Decrypt();

            // Iterate over all embedded file attachments.
            // EmbeddedFiles is a collection of FileSpecification objects.
            for (int i = 1; i <= doc.EmbeddedFiles.Count; i++) // 1‑based indexing as required by Aspose.Pdf
            {
                FileSpecification spec = doc.EmbeddedFiles[i];
                string fileName = spec.Name; // original attachment name

                // Build full path for the extracted file.
                string outputPath = Path.Combine(outputFolder, fileName);

                // Save the attachment to disk using the Contents stream of the FileSpecification.
                if (spec.Contents != null)
                {
                    using (FileStream outStream = File.Create(outputPath))
                    {
                        spec.Contents.CopyTo(outStream);
                    }
                    Console.WriteLine($"Extracted: {fileName} → {outputPath}");
                }
                else
                {
                    Console.WriteLine($"Attachment '{fileName}' does not contain a content stream.");
                }
            }
        }

        Console.WriteLine("Attachment extraction completed.");
    }
}
