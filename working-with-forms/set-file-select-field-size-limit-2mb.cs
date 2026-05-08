using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Set the global file‑size limit for loading files into memory (megabytes).
        // This limit also applies to file‑select fields when a user uploads a file.
        Document.FileSizeLimitToMemoryLoading = 2; // 2 MB

        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, add a file‑select box (implemented as a TextBoxField) and save.
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing).
            Page page = doc.Pages[1];

            // Define the rectangle for the file‑select field.
            // Fully qualify to avoid ambiguity with System.Drawing.Rectangle.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 530);

            // FileSelectBoxField has no public constructors, so use TextBoxField instead.
            TextBoxField fileField = new TextBoxField(page, rect)
            {
                PartialName   = "UploadFile",
                AlternateName = "Upload a file (max 2 MB)",
                // Optional: limit the length of the file path string.
                MaxLen = 255
            };

            // Add the field to the document's form collection.
            doc.Form.Add(fileField);

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with file‑size limit set to 2 MB: {outputPath}");
    }
}
