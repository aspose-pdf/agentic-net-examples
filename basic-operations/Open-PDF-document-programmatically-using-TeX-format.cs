using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf core library is required

class Program
{
    static void Main()
    {
        // ---------------------------------------------------------------------
        // Resolve the directory that contains the TeX source file.
        // Here we assume a sub‑folder named "Data" next to the executable.
        // Adjust the path as needed for your environment.
        // ---------------------------------------------------------------------
        string dataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");

        // Verify that the directory exists.
        if (!Directory.Exists(dataDir))
        {
            Console.WriteLine($"Data directory does not exist: {dataDir}");
            return;
        }

        // Build full paths for the input TeX file and the output PDF file.
        string texFile = Path.Combine(dataDir, "TeX-to-PDF.tex");
        string pdfFile = Path.Combine(dataDir, "Tex-to-PDF.pdf");

        // Ensure the TeX source file is present before attempting conversion.
        if (!File.Exists(texFile))
        {
            Console.WriteLine($"TeX source file not found: {texFile}");
            return;
        }

        try
        {
            // ---------------------------------------------------------------
            // Load the TeXLoadOptions type via reflection. This avoids a
            // compile‑time dependency on the Aspose.Pdf.TeX assembly. At
            // runtime the assembly must be present (install the NuGet
            // package Aspose.Pdf.TeX).
            // ---------------------------------------------------------------
            var texLoadOptionsType = Type.GetType("Aspose.Pdf.TeX.TeXLoadOptions, Aspose.Pdf.TeX");
            if (texLoadOptionsType == null)
            {
                Console.WriteLine("Aspose.Pdf.TeX assembly not found. Please install the Aspose.Pdf.TeX NuGet package.");
                return;
            }

            // Create an instance of TeXLoadOptions (default constructor).
            var texLoadOptions = Activator.CreateInstance(texLoadOptionsType);

            // Find the Document constructor that accepts (string, TeXLoadOptions).
            var ctor = typeof(Document).GetConstructor(new[] { typeof(string), texLoadOptionsType });
            if (ctor == null)
            {
                Console.WriteLine("Unable to locate the Document constructor that accepts TeXLoadOptions.");
                return;
            }

            // Create the PDF document using the reflected constructor.
            using (Document pdfDocument = (Document)ctor.Invoke(new object[] { texFile, texLoadOptions }))
            {
                // Save the generated PDF.
                pdfDocument.Save(pdfFile);
            }

            Console.WriteLine($"TeX file successfully converted to PDF: {pdfFile}");
        }
        catch (Exception ex)
        {
            // Catch any conversion‑related exceptions and display a friendly message.
            Console.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
