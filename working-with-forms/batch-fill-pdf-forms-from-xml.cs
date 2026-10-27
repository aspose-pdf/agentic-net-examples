using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class BatchXmlFormImporter
{
    static void Main()
    {
        // Base directory of the running application – works on any OS
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Resolve input / output folders (fallback to the base directory if the configured one does not exist)
        string pdfFolder = GetValidDirectory(Path.Combine(baseDir, "Input", "PDFs"), "PDF input");
        string xmlFolder = GetValidDirectory(Path.Combine(baseDir, "Input", "XML"), "XML input");
        string outputFolder = Path.Combine(baseDir, "Output", "FilledPDFs");
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the source folder
        string[] pdfFiles = Directory.GetFiles(pdfFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{pdfFolder}'. Nothing to process.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Determine corresponding XML file (same file name, .xml extension)
                string baseName = Path.GetFileNameWithoutExtension(pdfPath);
                string xmlPath = Path.Combine(xmlFolder, baseName + ".xml");

                if (!File.Exists(xmlPath))
                {
                    Console.WriteLine($"XML data not found for '{baseName}'. Skipping.");
                    continue;
                }

                // Load PDF, import XML form data, and save the result
                using (Document doc = new Document(pdfPath))
                {
                    ImportFormDataFromXml(doc, xmlPath);

                    string outputPdfPath = Path.Combine(outputFolder, baseName + "_filled.pdf");
                    doc.Save(outputPdfPath);
                    Console.WriteLine($"Processed '{baseName}': saved filled PDF to '{outputPdfPath}'.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[Error] Failed to process '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch import completed.");
    }

    /// <summary>
    /// Returns a valid directory path. If the supplied path does not exist, the method falls back to the
    /// application base directory and logs the situation.
    /// </summary>
    private static string GetValidDirectory(string path, string description)
    {
        if (Directory.Exists(path))
            return path;

        string fallback = AppDomain.CurrentDomain.BaseDirectory;
        Console.WriteLine($"{description} folder not found at '{path}'. Using fallback folder '{fallback}'.");
        return fallback;
    }

    /// <summary>
    /// Reads a simple XML file and maps its field values to the PDF form fields.
    /// Expected XML structure (example):
    /// <form>
    ///   <field name="FirstName">John</field>
    ///   <field name="LastName">Doe</field>
    /// </form>
    /// </summary>
    private static void ImportFormDataFromXml(Document doc, string xmlPath)
    {
        if (doc?.Form?.Fields == null)
            return;

        XDocument xDoc = XDocument.Load(xmlPath);
        // Iterate over each <field> element (case‑insensitive name handling)
        foreach (var fieldElem in xDoc.Descendants("field"))
        {
            var nameAttr = fieldElem.Attribute("name") ?? fieldElem.Attribute("Name");
            if (nameAttr == null)
                continue; // No field name – cannot map

            string fieldName = nameAttr.Value;
            string fieldValue = fieldElem.Value ?? string.Empty;

            // Find the matching PDF form field (case‑insensitive match)
            var pdfField = doc.Form.Fields.FirstOrDefault(f =>
                string.Equals(f.Name, fieldName, StringComparison.OrdinalIgnoreCase));

            if (pdfField != null)
            {
                pdfField.Value = fieldValue;
            }
        }
    }
}
