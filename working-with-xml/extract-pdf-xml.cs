using System;
using System.IO;
using Aspose.Pdf;

namespace ExtractPdfXmlExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Determine an absolute path for the sample PDF to avoid UriFormatException
            string pdfPath = Path.GetFullPath("sample.pdf");
            string xmlPath = Path.GetFullPath("sample.xml");

            // Create a sample PDF document
            using (Document sampleDoc = new Document())
            {
                // Add a blank page to the document
                sampleDoc.Pages.Add();

                // Save the sample PDF to the absolute path
                sampleDoc.Save(pdfPath);
            }

            // Load the PDF document and extract its XML representation
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Save the XML representation of the PDF to the absolute path
                pdfDoc.SaveXml(xmlPath);
            }

            Console.WriteLine("XML extraction completed.");
        }
    }
}
