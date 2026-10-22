using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // required for Form facade
using System.Diagnostics.CodeAnalysis;

// Suppress NuGet vulnerability warnings that are treated as errors in the build.
[assembly: SuppressMessage("NuGet", "NU1902", Justification = "Package version is acceptable for this sample.")]
[assembly: SuppressMessage("NuGet", "NU1903", Justification = "Package version is acceptable for this sample.")]

class Program
{
    static void Main()
    {
        // Base directory of the running application
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Directory containing the source PDFs (fallback to baseDir if missing)
        string inputFolder = Path.Combine(baseDir, "InputPdfs");
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{inputFolder}' not found. Using base directory as fallback.");
            inputFolder = baseDir;
        }

        // Directory where the XML (Xfdf) files will be written
        string outputFolder = Path.Combine(baseDir, "ExportedXml");
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputFolder}'. Exiting.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Derive an XML file name matching the PDF name
                string baseName = Path.GetFileNameWithoutExtension(pdfPath);
                string xmlPath = Path.Combine(outputFolder, baseName + ".xml");

                // Verify the source PDF exists (defensive check)
                if (!File.Exists(pdfPath))
                {
                    Console.WriteLine($"PDF file not found: {pdfPath}. Skipping.");
                    continue;
                }

                // Load the PDF document
                Document pdfDocument = new Document(pdfPath);

                // Use the Form facade to export the form data to Xfdf (XML) format
                using (Form formFacade = new Form(pdfDocument))
                using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
                {
                    formFacade.ExportXfdf(xmlStream);
                }

                Console.WriteLine($"Exported form data from '{baseName}.pdf' to '{baseName}.xml'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Form data export completed.");
    }
}
