using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Export the PDF to its XML representation in memory
            using (MemoryStream xmlStream = new MemoryStream())
            {
                pdfDoc.Save(xmlStream, new XmlSaveOptions());
                xmlStream.Position = 0;

                // Read the XML content as a string
                string xmlContent;
                using (StreamReader reader = new StreamReader(xmlStream))
                {
                    xmlContent = reader.ReadToEnd();
                }

                // Define the Graph element with Circle and GraphInfo children
                const string graphXml = "<Graph><Circle/><GraphInfo/></Graph>";

                // Insert the new Graph element before the closing </Document> tag
                int insertPos = xmlContent.LastIndexOf("</Document>", StringComparison.Ordinal);
                if (insertPos >= 0)
                {
                    xmlContent = xmlContent.Insert(insertPos, graphXml);
                }
                else
                {
                    Console.Error.WriteLine("Invalid XML structure: missing </Document> tag.");
                    return;
                }

                // Write the modified XML to a temporary file
                string tempXmlPath = Path.Combine(Path.GetTempPath(), "modified.xml");
                File.WriteAllText(tempXmlPath, xmlContent);

                // Bind the modified XML back to the PDF document
                pdfDoc.BindXml(tempXmlPath);
            }

            // Save the updated PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with inserted Graph element: {outputPdfPath}");
    }
}