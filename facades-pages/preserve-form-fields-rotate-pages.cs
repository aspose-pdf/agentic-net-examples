using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        // Load the PDF document
        using (Document document = new Document(inputPath))
        {
            // Export existing form fields to a memory stream (FDF format)
            Form formExporter = new Form(document);
            using (MemoryStream fdfStream = new MemoryStream())
            {
                formExporter.ExportFdf(fdfStream);
                fdfStream.Position = 0;

                // Rotate each page 90 degrees clockwise using the correct enum value
                foreach (Page page in document.Pages)
                {
                    page.Rotate = Rotation.on90; // <-- fixed
                }

                // Import the form fields back into the rotated document
                Form formImporter = new Form(document);
                formImporter.ImportFdf(fdfStream);
            }

            // Save the rotated PDF with preserved form fields
            document.Save(outputPath);
        }

        Console.WriteLine("Rotated PDF saved to '" + outputPath + "'.");
    }
}
