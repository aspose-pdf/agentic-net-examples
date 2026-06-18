using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Step 1: Create a sample PDF that uses the Courier font.
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            TextFragment fragment = new TextFragment("This text uses Courier font.");
            fragment.TextState.Font = FontRepository.FindFont("Courier");
            fragment.TextState.FontSize = 12;
            page.Paragraphs.Add(fragment);
            doc.Save("input.pdf");
        }

        // Step 2: Register a font substitution – replace Courier with Liberation Mono.
        SimpleFontSubstitution substitution = new SimpleFontSubstitution("Courier", "LiberationMono");
        FontRepository.Substitutions.Add(substitution);

        // Step 3: Convert the PDF to a multi‑page TIFF image.
        using (PdfConverter converter = new PdfConverter())
        {
            converter.BindPdf("input.pdf");
            // Optional: set a higher resolution for better quality.
            converter.Resolution = new Resolution(300);
            converter.DoConvert();
            converter.SaveAsTIFF("output.tiff");
        }
    }
}