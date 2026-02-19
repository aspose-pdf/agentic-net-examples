using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: <input.pcl> <iccProfile.icc> <output.pdf>
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <input.pcl> <iccProfile.icc> <output.pdf>");
            return;
        }

        string pclPath = args[0];
        string iccPath = args[1];
        string outputPath = args[2];

        // Verify that the input files exist
        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"PCL file not found: {pclPath}");
            return;
        }

        if (!File.Exists(iccPath))
        {
            Console.Error.WriteLine($"ICC profile file not found: {iccPath}");
            return;
        }

        try
        {
            // Load the PCL file using default load options
            PclLoadOptions pclLoadOptions = new PclLoadOptions();
            Document pdfDocument = new Document(pclPath, pclLoadOptions);

            // Configure conversion to PDF/X-4 and attach the external ICC profile
            var conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_4)
            {
                IccProfileFileName = iccPath
            };

            // Perform the conversion to PDF/X-4
            pdfDocument.Convert(conversionOptions);

            // Save the resulting PDF/X-4 document (still saved as regular PDF format)
            pdfDocument.Save(outputPath, SaveFormat.Pdf);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
