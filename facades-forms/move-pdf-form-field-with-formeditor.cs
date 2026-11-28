using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "moved4.pdf";

        // Verify the source PDF exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize FormEditor and bind the PDF document
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPath);

            // New position for the field "Phone" (llx, lly, urx, ury)
            // FormEditor.MoveField expects four float parameters, not a Rectangle.
            formEditor.MoveField("Phone", 160f, 210f, 260f, 310f);

            // Save the updated PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Field 'Phone' moved and saved to '{outputPath}'.");
    }
}
