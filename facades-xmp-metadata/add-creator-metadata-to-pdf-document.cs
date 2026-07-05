using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public static class PdfDocumentExtensions
{
    /// <summary>
    /// Adds a Creator metadata value to any Aspose.Pdf.Document.
    /// The change is applied to the in‑memory Document instance; you can later call <c>pdfDocument.Save(...)</c>.
    /// </summary>
    /// <param name="pdfDocument">The PDF document to modify.</param>
    /// <param name="creator">The creator string to set.</param>
    public static void AddCreatorTool(this Document pdfDocument, string creator)
    {
        // Initialise the PdfFileInfo facade.
        PdfFileInfo fileInfo = new PdfFileInfo();

        // Bind the facade to the existing Document instance.
        fileInfo.BindPdf(pdfDocument);

        // Set the Creator property; this updates the metadata of the bound document.
        fileInfo.Creator = creator;

        // No explicit Save is required here; the change is reflected in the Document object.
        // The caller can later call pdfDocument.Save(...) as needed.
    }
}

// ---------------------------------------------------------------------------
// A minimal entry point is required because the project is built as an
// executable (Console Application). Adding a static Main method satisfies the
// compiler and allows the library to be compiled and, if desired, executed.
// ---------------------------------------------------------------------------
public class Program
{
    public static void Main(string[] args)
    {
        // Example usage (optional – can be removed in production).
        // Ensure you have a valid PDF file at the specified path.
        // Document doc = new Document("input.pdf");
        // doc.AddCreatorTool("My Awesome Tool");
        // doc.Save("output.pdf");
        
        // The method is intentionally left empty to serve only as a valid entry point.
    }
}
