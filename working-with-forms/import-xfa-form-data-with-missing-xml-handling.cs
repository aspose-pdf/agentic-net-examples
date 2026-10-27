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
        const string pdfPath    = "form.pdf";
        const string xmlPath    = "data.xml";
        const string outputPath = "filled_form.pdf";

        // Verify the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – '{pdfPath}'.");
            return;
        }

        // Load the PDF document inside a using block (deterministic disposal)
        using (Document doc = new Document(pdfPath))
        {
            // Attempt to load form data from the XML file if it exists
            if (File.Exists(xmlPath))
            {
                try
                {
                    // Load XML and map values to form fields manually (ImportXml is not available in core API)
                    XDocument xDoc = XDocument.Load(xmlPath);
                    foreach (XElement element in xDoc.Root.Elements())
                    {
                        string fieldName = element.Name.LocalName;
                        string fieldValue = element.Value ?? string.Empty;

                        // Find the field in the PDF form collection
                        var field = doc.Form?.Fields?.FirstOrDefault(f => f.Name == fieldName || f.FullName == fieldName);
                        if (field != null)
                        {
                            field.Value = fieldValue;
                        }
                        else
                        {
                            Console.WriteLine($"Warning: Field '{fieldName}' not found in PDF form.");
                        }
                    }
                }
                catch (Exception ex) when (ex is PdfException || ex is InvalidOperationException || ex is IOException || ex is System.Xml.XmlException)
                {
                    // Handle errors related to XML loading or PDF field assignment
                    Console.Error.WriteLine($"Failed to import XML form data: {ex.Message}");
                }
            }
            else
            {
                // XML file is missing – skip loading but continue processing
                Console.WriteLine($"Warning: XML data file '{xmlPath}' not found. Proceeding without importing form data.");
            }

            // Save the (potentially) updated PDF
            try
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error saving PDF: {ex.Message}");
            }
        }
    }
}
