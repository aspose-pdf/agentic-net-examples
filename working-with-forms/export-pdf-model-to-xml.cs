using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for Pdf3DCrossSection if needed

class Program
{
    static void Main()
    {
        // Directory containing the PDF file.
        string dataDir = "YOUR_DATA_DIRECTORY";

        // Input PDF and output XML paths.
        string pdfPath = Path.Combine(dataDir, "input.pdf");
        string xmlPath = Path.Combine(dataDir, "output.xml");

        // Verify the input file exists.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // OPTIONAL: Adjust visibility of 3‑D cross‑section annotations.
            // This demonstrates accessing the Visibility property.
            // foreach (var annotation in pdfDoc.Pages[1].Annotations)
            // {
            //     if (annotation is Pdf3DCrossSection crossSection)
            //     {
            //         crossSection.Visibility = true; // or false as required
            //     }
            // }

            // Save the document model to XML, preserving all state (including visibility).
            XmlSaveOptions saveOptions = new XmlSaveOptions();
            pdfDoc.Save(xmlPath, saveOptions);
        }

        Console.WriteLine($"Document model exported to XML: {xmlPath}");
    }
}