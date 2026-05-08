using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string inputPdfPath   = "input.pdf";
        const string intermediateXmlPath = "styled.xml";
        const string outputPdfPath  = "output_custom_colors.pdf";

        // Verify the input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // ------------------------------------------------------------
        // Step 1: Load the original PDF document (core API only)
        // ------------------------------------------------------------
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // ------------------------------------------------------------
            // Step 2: Export the PDF structure to XML (so we can edit styles)
            // ------------------------------------------------------------
            XmlSaveOptions xmlSaveOpts = new XmlSaveOptions();
            pdfDoc.Save(intermediateXmlPath, xmlSaveOpts);
        }

        // ------------------------------------------------------------
        // Step 3: Modify the exported XML to apply a custom color scheme.
        //         This example assumes the XML contains elements with a
        //         "Color" attribute that can be changed. In a real scenario
        //         you would load the XML, edit the attribute values, and
        //         save it back. Here we simply place a placeholder comment.
        // ------------------------------------------------------------
        if (File.Exists(intermediateXmlPath))
        {
            string xmlContent = File.ReadAllText(intermediateXmlPath);

            // Example: replace all occurrences of a default color (e.g., "#000000")
            // with a custom dark blue color ("#003366").
            // Adjust the pattern according to the actual XML schema.
            string updatedXml = xmlContent.Replace("#000000", "#003366");

            // Write the modified XML back to the same file
            File.WriteAllText(intermediateXmlPath, updatedXml);
        }
        else
        {
            Console.Error.WriteLine($"Failed to create intermediate XML: {intermediateXmlPath}");
            return;
        }

        // ------------------------------------------------------------
        // Step 4: Load the modified XML back into a PDF document.
        //         XmlLoadOptions tells Aspose.Pdf to treat the XML as a
        //         source format (PDFXML) and convert it to PDF.
        // ------------------------------------------------------------
        XmlLoadOptions xmlLoadOpts = new XmlLoadOptions();
        using (Document styledPdf = new Document(intermediateXmlPath, xmlLoadOpts))
        {
            // ------------------------------------------------------------
            // Optional: verify that the document has pages before saving
            // ------------------------------------------------------------
            if (styledPdf?.Pages?.Count > 0)
            {
                // ------------------------------------------------------------
                // Step 5: Save the final PDF with the custom color scheme applied
                // ------------------------------------------------------------
                styledPdf.Save(outputPdfPath);
                Console.WriteLine($"Custom-colored PDF saved to '{outputPdfPath}'.");
            }
            else
            {
                Console.Error.WriteLine("The styled PDF has no pages; conversion may have failed.");
            }
        }
    }
}