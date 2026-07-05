using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Set the global limit for loading files into memory (value is in megabytes)
        Document.FileSizeLimitToMemoryLoading = 2; // 2 MB

        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle for the file‑attachment field (implemented as a text box)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a TextBoxField – FileSelectBoxField has no public constructors
            TextBoxField fileField = new TextBoxField(doc.Pages[1], fieldRect)
            {
                PartialName = "AttachmentField", // field name
                MaxLen = 0                       // no limit on file name length
            };

            // Add the field to the document's form collection
            doc.Form.Add(fileField);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with a 2 MB file size limit: {outputPath}");
    }
}
