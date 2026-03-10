using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";
        const string outputDir  = "output";

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the source PDF inside a using block for deterministic disposal
        using (Document srcDoc = new Document(sourcePath))
        {
            // Optional: clean up unused resources to keep fidelity high
            srcDoc.OptimizeResources();

            // -------------------------------------------------
            // 1. Convert to PDF/A‑1b
            // -------------------------------------------------
            var pdfaOptions = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
            srcDoc.Convert(pdfaOptions);
            srcDoc.Save(Path.Combine(outputDir, "output_pdfa1b.pdf"));

            // -------------------------------------------------
            // 2. Convert to PDF/X‑3
            // -------------------------------------------------
            var pdfxOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_3, ConvertErrorAction.Delete);
            srcDoc.Convert(pdfxOptions);
            srcDoc.Save(Path.Combine(outputDir, "output_pdfx3.pdf"));

            // -------------------------------------------------
            // 3. Save as HTML (explicit HtmlSaveOptions required)
            // -------------------------------------------------
            var htmlOpts = new HtmlSaveOptions
            {
                PartsEmbeddingMode     = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
            };
            srcDoc.Save(Path.Combine(outputDir, "output.html"), htmlOpts);

            // -------------------------------------------------
            // 4. Save as EPUB (Flow recognition mode)
            // -------------------------------------------------
            var epubOpts = new EpubSaveOptions
            {
                ContentRecognitionMode = EpubSaveOptions.RecognitionMode.Flow
            };
            srcDoc.Save(Path.Combine(outputDir, "output.epub"), epubOpts);

            // -------------------------------------------------
            // 5. Save as DOCX (DocSaveOptions)
            // -------------------------------------------------
            var docOpts = new DocSaveOptions
            {
                Format = DocSaveOptions.DocFormat.DocX
            };
            srcDoc.Save(Path.Combine(outputDir, "output.docx"), docOpts);
        }

        // -------------------------------------------------
        // Use PdfFileEditor (a Facade) to split the original PDF
        // into single‑page PDFs. This demonstrates Facades usage.
        // -------------------------------------------------
        var editor = new PdfFileEditor();
        string splitTemplate = Path.Combine(outputDir, "page_%NUM%.pdf");
        editor.SplitToPages(sourcePath, splitTemplate);
    }
}