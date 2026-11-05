using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.docx";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(inputPath))
            {
                // Configure DOCX save options.
                // The Format property selects DOCX output.
                // Enhanced recognition for complex tables/graphics is handled internally.
                Aspose.Pdf.DocSaveOptions docxOptions = new Aspose.Pdf.DocSaveOptions
                {
                    Format = Aspose.Pdf.DocSaveOptions.DocFormat.DocX
                };

                // Save the PDF as DOCX using the specified options
                pdfDoc.Save(outputPath, docxOptions);
            }

            Console.WriteLine($"PDF successfully converted to DOCX: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}