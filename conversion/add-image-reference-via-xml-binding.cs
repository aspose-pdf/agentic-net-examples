using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";      // source tagged PDF
        const string outputPdfPath = "output.pdf";     // result PDF after binding
        const string tempXmlPath   = "temp.xml";       // intermediate XML file
        const string imageFilePath = "image.jpg";      // image to reference in XML

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(imageFilePath))
        {
            Console.Error.WriteLine($"Image file not found: {imageFilePath}");
            return;
        }

        try
        {
            // Load the tagged PDF
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Export the PDF to XML (requires a tagged PDF)
                using (FileStream xmlStream = new FileStream(tempXmlPath, FileMode.Create, FileAccess.Write))
                {
                    XmlSaveOptions xmlOpts = new XmlSaveOptions();
                    pdfDoc.Save(xmlStream, xmlOpts);
                }

                // Load the generated XML, add an <Image File="..."/> element
                XDocument xmlDoc = XDocument.Load(tempXmlPath);
                XElement imageElement = new XElement("Image",
                    new XAttribute("File", imageFilePath));

                // Append the new element to the root (adjust placement as needed)
                xmlDoc.Root?.Add(imageElement);

                // Save the modified XML back to a temporary file
                string modifiedXmlPath = "modified.xml";
                xmlDoc.Save(modifiedXmlPath);

                // Bind the modified XML back to the PDF document
                pdfDoc.BindXml(modifiedXmlPath);

                // Save the updated PDF
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF saved with bound image XML: {outputPdfPath}");
        }
        catch (PdfException pdfEx)
        {
            Console.Error.WriteLine($"Aspose.Pdf error: {pdfEx.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
        finally
        {
            // Clean up temporary files
            try { if (File.Exists(tempXmlPath)) File.Delete(tempXmlPath); } catch { }
            try { if (File.Exists("modified.xml")) File.Delete("modified.xml"); } catch { }
        }
    }
}