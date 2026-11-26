using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string xmlTemplatePath = "template.xml";
        const string outputPdfPath   = "output.pdf";
        const string placeholderText = "segment1"; // the text placed in the template to be replaced
        const string newText         = "Updated text content";

        if (!File.Exists(xmlTemplatePath))
        {
            Console.Error.WriteLine($"Template not found: {xmlTemplatePath}");
            return;
        }

        // Load the XML template as a PDF document
        XmlLoadOptions loadOptions = new XmlLoadOptions();
        using (Document pdfDoc = new Document(xmlTemplatePath, loadOptions))
        {
            // Extract all text fragments from the document
            TextFragmentAbsorber absorber = new TextFragmentAbsorber
            {
                ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            };
            pdfDoc.Pages.Accept(absorber);

            // Find the fragment that contains the placeholder text
            TextFragment fragment = absorber.TextFragments
                                            .FirstOrDefault(tf => tf.Text?.Equals(placeholderText, StringComparison.Ordinal) == true);

            if (fragment != null)
            {
                fragment.Text = newText;
                Console.WriteLine($"Replaced placeholder '{placeholderText}' with new text.");
            }
            else
            {
                Console.WriteLine($"TextFragment with placeholder '{placeholderText}' not found.");
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved to '{outputPdfPath}'.");
    }
}
