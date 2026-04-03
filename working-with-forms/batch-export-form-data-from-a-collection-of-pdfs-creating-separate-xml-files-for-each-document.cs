using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class BatchFormExport
{
    static void Main()
    {
        // Folder containing the source PDF files
        // Use a fully‑qualified absolute path to avoid platform‑specific path issues.
        // On Windows you can keep the literal "C:\PdfInputs"; on other OS the path will be resolved relative to the current directory.
        string inputFolder = GetAbsolutePath(@"C:\PdfInputs");
        // Folder where the XML (XFDF) files will be written
        string outputFolder = GetAbsolutePath(@"C:\PdfFormXml");

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Verify the input directory exists before trying to enumerate files
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.Error.WriteLine("No PDF files found in the input folder.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Load the PDF document (using the lifecycle rule: load within a using block)
                using (Document doc = new Document(pdfPath))
                {
                    // Build the output XML file name – same base name with .xml extension
                    string xmlFileName = Path.GetFileNameWithoutExtension(pdfPath) + ".xml";
                    string xmlPath = Path.Combine(outputFolder, xmlFileName);

                    // Export form annotations (XFDF) to an XML file.
                    // XFDF is an XML representation of form data, satisfying the requirement.
                    doc.ExportAnnotationsToXfdf(xmlPath);
                }

                Console.WriteLine($"Exported form data to XML: {Path.GetFileName(pdfPath)} -> {Path.GetFileNameWithoutExtension(pdfPath)}.xml");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Returns an absolute path. If the supplied path is already absolute it is returned unchanged.
    /// If it is relative, it is combined with the current working directory.
    /// This prevents the "C:\PdfInputs" literal from being interpreted as a relative path on non‑Windows platforms.
    /// </summary>
    private static string GetAbsolutePath(string path)
    {
        if (Path.IsPathRooted(path))
            return Path.GetFullPath(path);
        return Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, path));
    }
}
