using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.xfdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            // Create a stream for the XFDF output
            using (FileStream xfdfStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                // Use the Form facade to export form data to XFDF
                Form formFacade = new Form();
                formFacade.BindPdf(doc);
                // Export the form data to the XFDF stream
                formFacade.ExportXfdf(xfdfStream);
            }

            Console.WriteLine($"Form data successfully exported to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
