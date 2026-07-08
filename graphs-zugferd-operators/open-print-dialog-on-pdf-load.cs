using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // NamedAction, PredefinedAction

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_print.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Set an open action that shows the print dialog when the PDF is opened
            doc.OpenAction = new NamedAction(PredefinedAction.PrintDialog);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with automatic print dialog: {outputPath}");
    }
}