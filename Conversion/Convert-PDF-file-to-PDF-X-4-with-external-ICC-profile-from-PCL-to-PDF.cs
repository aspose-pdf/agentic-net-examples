using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PCL file, output PDF/X-4 file, and external ICC profile
        const string pclPath = "input.pcl";
        const string outputPdfPath = "output_pdfx4.pdf";
        const string iccProfilePath = "profile.icc";

        // Verify that required files exist
        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"PCL file not found: {pclPath}");
            return;
        }
        if (!File.Exists(iccProfilePath))
        {
            Console.Error.WriteLine($"ICC profile not found: {iccProfilePath}");
            return;
        }

        // Load the PCL file using PclLoadOptions
        PclLoadOptions loadOptions = new PclLoadOptions();

        // Ensure the Document is disposed properly
        using (Document doc = new Document(pclPath, loadOptions))
        {
            // Configure conversion to PDF/X-4 and attach the ICC profile
            PdfFormatConversionOptions convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_4);
            convOptions.IccProfileFileName = iccProfilePath;
            convOptions.ErrorAction = ConvertErrorAction.Delete; // handle conversion errors

            // Perform the conversion
            doc.Convert(convOptions);

            // Save the resulting PDF/X-4 document
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Converted PDF/X-4 saved to '{outputPdfPath}'.");
    }
}