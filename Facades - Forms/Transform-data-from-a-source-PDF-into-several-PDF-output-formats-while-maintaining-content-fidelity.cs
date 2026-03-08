using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facade classes (e.g., PdfConverter) are available if needed

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string sourcePdf = "source.pdf";

        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }

        // Load the source PDF using the core Document API (required for saving to other formats)
        using (Document pdfDoc = new Document(sourcePdf))
        {
            // ---------- Save as HTML ----------
            const string htmlPath = "output.html";
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                // Embed all resources into a single HTML file
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                // Render raster images as PNG embedded into SVG (preserves quality)
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
            };
            pdfDoc.Save(htmlPath, htmlOpts);

            // ---------- Save as DOCX ----------
            const string docxPath = "output.docx";
            DocSaveOptions docxOpts = new DocSaveOptions
            {
                // Specify the DOCX format explicitly
                Format = DocSaveOptions.DocFormat.DocX
            };
            pdfDoc.Save(docxPath, docxOpts);

            // ---------- Save as XLSX ----------
            const string xlsxPath = "output.xlsx";
            ExcelSaveOptions xlsxOpts = new ExcelSaveOptions
            {
                // Specify the XLSX format explicitly
                Format = ExcelSaveOptions.ExcelFormat.XLSX
            };
            pdfDoc.Save(xlsxPath, xlsxOpts);

            // ---------- Save as PPTX ----------
            const string pptxPath = "output.pptx";
            PptxSaveOptions pptxOpts = new PptxSaveOptions(); // No special settings required
            pdfDoc.Save(pptxPath, pptxOpts);

            // ---------- Save as EPUB ----------
            const string epubPath = "output.epub";
            EpubSaveOptions epubOpts = new EpubSaveOptions
            {
                // Use the flow recognition mode for best content fidelity
                ContentRecognitionMode = EpubSaveOptions.RecognitionMode.Flow
            };
            pdfDoc.Save(epubPath, epubOpts);

            // ---------- Save as XML ----------
            const string xmlPath = "output.xml";
            XmlSaveOptions xmlOpts = new XmlSaveOptions(); // Default options preserve structure
            pdfDoc.Save(xmlPath, xmlOpts);
        }

        // Optional: Convert each page to an image using the PdfConverter facade.
        // This demonstrates that the visual appearance can be retained as raster images.
        // Note: Image conversion relies on GDI+ and is therefore Windows‑only.
        // If running on macOS/Linux, comment out the block below.
        /*
        using (PdfConverter converter = new PdfConverter())
        {
            converter.BindPdf(sourcePdf);
            converter.StartPage = 1;
            converter.EndPage = converter.PageCount; // Process all pages
            converter.Resolution = 150; // Default resolution; increase for higher fidelity

            int pageIndex = 1;
            while (converter.HasNextImage())
            {
                string imagePath = $"page_{pageIndex}.png";
                // Save each page as PNG (default image format is JPEG; specify PNG for lossless output)
                converter.GetNextImage(imagePath, System.Drawing.Imaging.ImageFormat.Png);
                pageIndex++;
            }

            converter.Close();
        }
        */

        Console.WriteLine("All conversions completed successfully.");
    }
}