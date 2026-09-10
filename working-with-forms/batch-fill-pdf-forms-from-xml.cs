using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Base directory of the application (works for both Windows and Linux)
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Resolve input / output folders relative to the base directory
        string pdfFolder = Path.Combine(baseDir, "pdfs");
        string xmlFolder = Path.Combine(baseDir, "xmls");
        string outputFolder = Path.Combine(baseDir, "output");

        // Validate / create folders so the sample can run out‑of‑the‑box
        if (!Directory.Exists(pdfFolder))
        {
            Console.WriteLine($"PDF source folder not found: '{pdfFolder}'. Creating it now.");
            Directory.CreateDirectory(pdfFolder);
            Console.WriteLine("Place PDF files in this folder and re‑run the program.");
            return; // No PDFs to process yet
        }

        if (!Directory.Exists(xmlFolder))
        {
            Console.WriteLine($"XML source folder not found: '{xmlFolder}'. Creating it now.");
            Directory.CreateDirectory(xmlFolder);
            Console.WriteLine("Place matching XML files in this folder and re‑run the program.");
            // Continue – XML files may be added later; we will simply skip PDFs without XML.
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file found in the source folder
        foreach (string pdfPath in Directory.GetFiles(pdfFolder, "*.pdf"))
        {
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);
            string xmlPath = Path.Combine(xmlFolder, baseName + ".xml");
            string outputPath = Path.Combine(outputFolder, baseName + "_filled.pdf");

            if (!File.Exists(xmlPath))
            {
                Console.WriteLine($"[Warning] XML file not found for '{baseName}'. Skipping this PDF.");
                continue;
            }

            try
            {
                using (Document pdfDoc = new Document(pdfPath))
                {
                    // Bind the XML data to the PDF form fields
                    pdfDoc.BindXml(xmlPath);

                    // Save the filled PDF
                    pdfDoc.Save(outputPath);
                }

                Console.WriteLine($"[Success] Processed '{baseName}'. Output saved to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] Failed to process '{baseName}': {ex.Message}");
            }
        }
    }
}
