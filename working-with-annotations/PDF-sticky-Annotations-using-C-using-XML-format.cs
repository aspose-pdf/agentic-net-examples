using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text; // for Rectangle if needed (actually Rectangle is in Aspose.Pdf namespace)

// Load a PDF document from an XML source (PDFXML format)
string xmlInputPath = "input.pdfxml";   // source XML file containing PDF structure
string pdfOutputPath = "output_with_sticky.pdf";

if (!File.Exists(xmlInputPath))
{
    Console.Error.WriteLine($"XML source not found: {xmlInputPath}");
    return;
}

// Load the XML using the dedicated load options
PdfXmlLoadOptions xmlLoadOptions = new PdfXmlLoadOptions();
using (Document doc = new Document(xmlInputPath, xmlLoadOptions))
{
    // Ensure the document has at least one page
    if (doc.Pages.Count == 0)
    {
        // Add a blank page if the source XML did not contain any pages
        doc.Pages.Add();
    }

    // Create a sticky note (TextAnnotation) on the first page
    // Fully qualified Rectangle avoids ambiguity with System.Drawing
    Aspose.Pdf.Rectangle noteRect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);
    TextAnnotation sticky = new TextAnnotation(doc.Pages[1], noteRect)
    {
        Title = "Reviewer",
        Contents = "Please review this section.",
        Color = Aspose.Pdf.Color.Yellow, // highlight color of the note icon
        Open = true,                     // open the note by default
        Icon = TextIcon.Comment          // choose an appropriate icon
    };

    // Add the annotation to the page's annotation collection
    doc.Pages[1].Annotations.Add(sticky);

    // Save the modified document as a PDF (extension determines format)
    doc.Save(pdfOutputPath);
}

Console.WriteLine($"PDF with sticky annotation saved to '{pdfOutputPath}'.");