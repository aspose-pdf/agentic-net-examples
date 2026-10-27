using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Folder containing the source PDF files
        const string inputFolder = "InputPdfs";
        // Folder where the XML files will be written
        const string outputFolder = "FormXml";

        // Verify input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Load each PDF inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Skip files that do not contain any form fields
                    if (doc.Form == null || doc.Form.Count == 0)
                    {
                        Console.WriteLine($"No form fields in '{Path.GetFileName(pdfPath)}'. Skipping.");
                        continue;
                    }

                    // Build the output XML file name (same base name as PDF)
                    string xmlFileName = Path.GetFileNameWithoutExtension(pdfPath) + ".xml";
                    string xmlPath = Path.Combine(outputFolder, xmlFileName);

                    // Export the form data to XML (custom implementation – ExportToXml does not exist in core API)
                    ExportFormToXml(doc, xmlPath);

                    Console.WriteLine($"Exported form data to '{xmlPath}'.");
                }
            }
            catch (Exception ex)
            {
                // Log any errors but continue processing remaining files
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Serialises the AcroForm fields of a PDF document to a simple XML representation.
    /// The XML structure is:
    ///   <FormData>
    ///       <Field name="fieldName">fieldValue</Field>
    ///       ...
    ///   </FormData>
    /// </summary>
    /// <param name="doc">The loaded PDF document.</param>
    /// <param name="outputPath">Full path where the XML file will be saved.</param>
    private static void ExportFormToXml(Document doc, string outputPath)
    {
        // Guard against a missing Form object – though callers already check Count > 0.
        if (doc?.Form?.Fields == null)
        {
            // Create an empty XML file to keep the contract consistent.
            new XDocument(new XElement("FormData")).Save(outputPath);
            return;
        }

        var fieldElements = doc.Form.Fields
            .Select(field => new XElement(
                "Field",
                new XAttribute("name", field.Name ?? field.FullName ?? string.Empty),
                field.Value?.ToString() ?? string.Empty));

        var root = new XElement("FormData", fieldElements);
        var xDoc = new XDocument(root);
        xDoc.Save(outputPath);
    }
}
