using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for text-related classes if needed

class ExportPdf
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the source PDF inside a using block to ensure deterministic disposal.
        using (Document doc = new Document(inputPdfPath))
        {
            // ---------- Export to HTML ----------
            // HTML conversion requires GDI+ and must be wrapped in try‑catch on non‑Windows platforms.
            using (FileStream htmlStream = new FileStream("output.html", FileMode.Create, FileAccess.Write))
            {
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                try
                {
                    doc.Save(htmlStream, htmlOpts);
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML export requires Windows (GDI+). Skipped on this platform.");
                }
            } // htmlStream is disposed here

            // ---------- Export to SVG ----------
            using (FileStream svgStream = new FileStream("output.svg", FileMode.Create, FileAccess.Write))
            {
                SvgSaveOptions svgOpts = new SvgSaveOptions();
                doc.Save(svgStream, svgOpts);
            } // svgStream is disposed here

            // ---------- Export to DOCX ----------
            using (FileStream docxStream = new FileStream("output.docx", FileMode.Create, FileAccess.Write))
            {
                DocSaveOptions docxOpts = new DocSaveOptions
                {
                    Format = DocSaveOptions.DocFormat.DocX
                };
                doc.Save(docxStream, docxOpts);
            } // docxStream is disposed here

            // ---------- Export to XLSX ----------
            using (FileStream xlsxStream = new FileStream("output.xlsx", FileMode.Create, FileAccess.Write))
            {
                ExcelSaveOptions xlsxOpts = new ExcelSaveOptions
                {
                    Format = ExcelSaveOptions.ExcelFormat.XLSX
                };
                doc.Save(xlsxStream, xlsxOpts);
            } // xlsxStream is disposed here

            // ---------- Export to PPTX ----------
            using (FileStream pptxStream = new FileStream("output.pptx", FileMode.Create, FileAccess.Write))
            {
                PptxSaveOptions pptxOpts = new PptxSaveOptions();
                doc.Save(pptxStream, pptxOpts);
            } // pptxStream is disposed here

            // ---------- Export to EPUB ----------
            using (FileStream epubStream = new FileStream("output.epub", FileMode.Create, FileAccess.Write))
            {
                EpubSaveOptions epubOpts = new EpubSaveOptions
                {
                    ContentRecognitionMode = EpubSaveOptions.RecognitionMode.Flow
                };
                doc.Save(epubStream, epubOpts);
            } // epubStream is disposed here

            // ---------- Export to XML ----------
            using (FileStream xmlStream = new FileStream("output.xml", FileMode.Create, FileAccess.Write))
            {
                XmlSaveOptions xmlOpts = new XmlSaveOptions();
                doc.Save(xmlStream, xmlOpts);
            } // xmlStream is disposed here
        } // Document is disposed here

        Console.WriteLine("All export operations completed. FileStreams have been closed.");
    }
}