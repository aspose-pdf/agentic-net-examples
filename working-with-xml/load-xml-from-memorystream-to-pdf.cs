using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Sample XML data – in a real scenario this could come from any source
        string xmlContent = "<root><message>Hello, PDF!</message></root>";
        byte[] xmlBytes = Encoding.UTF8.GetBytes(xmlContent);

        // Load the XML from a memory stream (as plain text) and create a PDF document with default settings
        using (MemoryStream xmlStream = new MemoryStream(xmlBytes))
        {
            // Read the XML string from the stream
            string xmlString;
            using (StreamReader reader = new StreamReader(xmlStream, Encoding.UTF8))
            {
                xmlString = reader.ReadToEnd();
            }

            // Create a new PDF document
            Document pdfDoc = new Document();
            // Add a page
            Page page = pdfDoc.Pages.Add();
            // Add the XML content as a text fragment
            TextFragment tf = new TextFragment(xmlString);
            page.Paragraphs.Add(tf);

            // Save the resulting PDF to a file
            pdfDoc.Save("output.pdf");
        }
    }
}
