using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input folders (relative to the executable directory)
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string pdfFolder = Path.Combine(baseDir, "pdfs");
        string xmlFolder = Path.Combine(baseDir, "xmls");
        string outputFolder = Path.Combine(baseDir, "output");
        Directory.CreateDirectory(outputFolder);

        // Enumerate all PDF files in the source folder
        string[] pdfFiles = Directory.GetFiles(pdfFolder, "*.pdf");

        foreach (string pdfPath in pdfFiles)
        {
            // Derive the base name (without extension) to locate the matching XML file
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);
            string xmlPath = Path.Combine(xmlFolder, baseName + ".xml");

            if (!File.Exists(xmlPath))
            {
                Console.Error.WriteLine($"XML file not found for '{baseName}'. Skipping.");
                continue;
            }

            string outputPath = Path.Combine(outputFolder, baseName + "_filled.pdf");

            try
            {
                // Load the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Enable automatic recalculation of JavaScript‑based fields (optional but common)
                    doc.Form.AutoRecalculate = true;

                    // Load XML data safely – ensure the document has a root element
                    XDocument xmlDoc = XDocument.Load(xmlPath);
                    if (xmlDoc.Root == null)
                    {
                        Console.Error.WriteLine($"XML file '{xmlPath}' does not contain a root element. Skipping.");
                        continue;
                    }

                    // The expected XML structure is a simple list of elements where the element name
                    // corresponds to the form field name and the element value is the field value.
                    // Example:
                    //   <FormData>
                    //       <FirstName>John</FirstName>
                    //       <LastName>Doe</LastName>
                    //   </FormData>
                    // Adjust the parsing logic if your XML differs.
                    foreach (XElement element in xmlDoc.Root.Elements())
                    {
                        string fieldName = element.Name.LocalName;
                        string fieldValue = element.Value ?? string.Empty;

                        // Find the field in the PDF form (case‑insensitive match on Name or FullName)
                        Field field = FindFieldByName(doc.Form, fieldName);
                        if (field != null)
                        {
                            // Most field types expose a Value property.
                            field.Value = fieldValue;
                        }
                        else
                        {
                            Console.Error.WriteLine($"Warning: Field '{fieldName}' not found in PDF '{baseName}'.");
                        }
                    }

                    // Save the resulting PDF (PDF format is default)
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Successfully processed '{baseName}'. Saved to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{baseName}': {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Searches the document's form for a field whose Name or FullName matches the supplied name.
    /// The search is case‑insensitive and returns the first match.
    /// </summary>
    private static Field FindFieldByName(Form form, string name)
    {
        if (form?.Fields == null)
            return null;

        foreach (Field f in form.Fields)
        {
            if (string.Equals(f.Name, name, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(f.FullName, name, StringComparison.OrdinalIgnoreCase))
            {
                return f;
            }
        }
        return null;
    }
}
