using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facades namespace included as requested

class Program
{
    static void Main()
    {
        const string pclPath = "input.pcl";
        const string outputPdfPath = "output_pdfx4.pdf";
        const string iccProfilePath = "sRGB.icc"; // external ICC profile

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
        PclLoadOptions pclLoadOptions = new PclLoadOptions();

        using (Document doc = new Document(pclPath, pclLoadOptions))
        {
            // Configure conversion to PDF/X-4 with external ICC profile
            PdfFormatConversionOptions convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_4);
            convOptions.ErrorAction = ConvertErrorAction.Delete; // handle conversion errors
            convOptions.IccProfileFileName = iccProfilePath;    // assign ICC profile
            convOptions.OutputIntent = new OutputIntent("CustomICC"); // optional output intent identifier

            // Perform the format conversion
            doc.Convert(convOptions);

            // Save the converted PDF/X-4 document
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Conversion completed: {outputPdfPath}");
    }
}