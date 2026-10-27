using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

namespace PdfFacadeDemo
{
    /// <summary>
    /// Provides a collection of PDF edit operations using Aspose.Pdf.Facades.
    /// Each method is documented with XML comments for API reference generation.
    /// </summary>
    public static class PdfFacadeOperations
    {
        /// <summary>
        /// Merges multiple PDF files into a single PDF document.
        /// </summary>
        /// <param name="inputFiles">Array of input PDF file paths to be merged. Order is preserved.</param>
        /// <param name="outputFile">Path of the resulting merged PDF file.</param>
        public static void MergePdf(string[] inputFiles, string outputFile)
        {
            if (inputFiles == null || inputFiles.Length == 0)
                throw new ArgumentException("At least one input file must be specified.", nameof(inputFiles));

            // PdfFileEditor is a Facade class for file-level operations.
            PdfFileEditor editor = new PdfFileEditor();
            editor.Concatenate(inputFiles, outputFile);
        }

        /// <summary>
        /// Splits a PDF document into individual pages and saves each page as a separate PDF file.
        /// </summary>
        /// <param name="inputFile">Path to the source PDF document.</param>
        /// <param name="outputDirectory">Directory where the split pages will be saved. Files are named <c>Page_1.pdf</c>, <c>Page_2.pdf</c>, etc.</param>
        public static void SplitPdf(string inputFile, string outputDirectory)
        {
            if (!File.Exists(inputFile))
                throw new FileNotFoundException("Input PDF not found.", inputFile);

            Directory.CreateDirectory(outputDirectory);
            // PdfFileEditor does not have a Split method; use SplitToPages instead.
            PdfFileEditor editor = new PdfFileEditor();
            MemoryStream[] pageStreams = editor.SplitToPages(inputFile);

            for (int i = 0; i < pageStreams.Length; i++)
            {
                string outPath = Path.Combine(outputDirectory, $"Page_{i + 1}.pdf");
                using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    pageStreams[i].WriteTo(fs);
                }
            }
        }

        /// <summary>
        /// Adds an image stamp to every page of a PDF document.
        /// </summary>
        /// <param name="inputFile">Path to the source PDF document.</param>
        /// <param name="stampImagePath">Path to the image file to be used as a stamp.</param>
        /// <param name="outputFile">Path of the PDF document after stamping.</param>
        public static void AddImageStamp(string inputFile, string stampImagePath, string outputFile)
        {
            if (!File.Exists(inputFile))
                throw new FileNotFoundException("Input PDF not found.", inputFile);
            if (!File.Exists(stampImagePath))
                throw new FileNotFoundException("Stamp image not found.", stampImagePath);

            // Load the PDF document inside a using block for deterministic disposal.
            using (Document doc = new Document(inputFile))
            {
                ImageStamp stamp = new ImageStamp(stampImagePath)
                {
                    Background = false,
                    Opacity = 0.5,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                // Apply the stamp to each page.
                foreach (Page page in doc.Pages)
                {
                    page.AddStamp(stamp);
                }

                doc.Save(outputFile);
            }
        }

        /// <summary>
        /// Adds a text annotation to the first page of a PDF document.
        /// </summary>
        /// <param name="inputFile">Path to the source PDF document.</param>
        /// <param name="outputFile">Path of the PDF document after adding the annotation.</param>
        /// <param name="text">Content of the annotation.</param>
        /// <param name="llx">Lower‑left X coordinate of the annotation rectangle (in points).</param>
        /// <param name="lly">Lower‑left Y coordinate of the annotation rectangle (in points).</param>
        /// <param name="urx">Upper‑right X coordinate of the annotation rectangle (in points).</param>
        /// <param name="ury">Upper‑right Y coordinate of the annotation rectangle (in points).</param>
        public static void AddTextAnnotation(string inputFile, string outputFile, string text,
                                             double llx, double lly, double urx, double ury)
        {
            if (!File.Exists(inputFile))
                throw new FileNotFoundException("Input PDF not found.", inputFile);

            using (Document doc = new Document(inputFile))
            {
                // Use Aspose.Pdf.Rectangle to avoid ambiguity with System.Drawing.Rectangle.
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                // Create a TextAnnotation and configure its properties.
                TextAnnotation annotation = new TextAnnotation(doc.Pages[1], rect)
                {
                    Title = "Note",
                    Contents = text,
                    Open = true,
                    Icon = TextIcon.Note
                };

                doc.Pages[1].Annotations.Add(annotation);
                doc.Save(outputFile);
            }
        }

        /// <summary>
        /// Encrypts a PDF document with the specified user and owner passwords.
        /// </summary>
        /// <param name="inputFile">Path to the source PDF document.</param>
        /// <param name="outputFile">Path of the encrypted PDF document.</param>
        /// <param name="userPassword">Password required to open the document.</param>
        /// <param name="ownerPassword">Password that grants full permissions.</param>
        /// <param name="permissions">Combination of <see cref="Permissions"/> flags that define allowed operations.</param>
        public static void EncryptPdf(string inputFile, string outputFile,
                                      string userPassword, string ownerPassword,
                                      Permissions permissions)
        {
            if (!File.Exists(inputFile))
                throw new FileNotFoundException("Input PDF not found.", inputFile);

            using (Document doc = new Document(inputFile))
            {
                // Use CryptoAlgorithm.AESx256 for strong encryption.
                doc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx256);
                doc.Save(outputFile);
            }
        }

        /// <summary>
        /// Decrypts a previously encrypted PDF document.
        /// </summary>
        /// <param name="encryptedFile">Path to the encrypted PDF document.</param>
        /// <param name="outputFile">Path of the decrypted PDF document.</param>
        /// <param name="userPassword">Password used to open the encrypted document.</param>
        public static void DecryptPdf(string encryptedFile, string outputFile, string userPassword)
        {
            if (!File.Exists(encryptedFile))
                throw new FileNotFoundException("Encrypted PDF not found.", encryptedFile);

            // The password is supplied when opening the document; Decrypt() takes no arguments.
            using (Document doc = new Document(encryptedFile, userPassword))
            {
                doc.Decrypt();
                doc.Save(outputFile);
            }
        }

        /// <summary>
        /// Converts a PDF document to HTML format. The conversion requires GDI+ and therefore works only on Windows.
        /// </summary>
        /// <param name="inputFile">Path to the source PDF document.</param>
        /// <param name="outputHtml">Path of the resulting HTML file.</param>
        public static void ConvertPdfToHtml(string inputFile, string outputHtml)
        {
            if (!File.Exists(inputFile))
                throw new FileNotFoundException("Input PDF not found.", inputFile);

            using (Document doc = new Document(inputFile))
            {
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                try
                {
                    doc.Save(outputHtml, htmlOptions);
                }
                catch (TypeInitializationException)
                {
                    // GDI+ is unavailable on non‑Windows platforms.
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped.");
                }
            }
        }

        /// <summary>
        /// Sets alternative text for all images in a PDF document, improving accessibility.
        /// </summary>
        /// <param name="inputFile">Path to the source PDF document.</param>
        /// <param name="outputFile">Path of the PDF document after updating image alternative text.</param>
        /// <param name="altText">Alternative text to assign to each image.</param>
        public static void SetAlternativeTextForImages(string inputFile, string outputFile, string altText)
        {
            if (!File.Exists(inputFile))
                throw new FileNotFoundException("Input PDF not found.", inputFile);

            using (Document doc = new Document(inputFile))
            {
                foreach (Page page in doc.Pages)
                {
                    // XImageCollection is enumerable; iterate directly.
                    foreach (XImage img in page.Resources.Images)
                    {
                        // The method requires the page reference.
                        img.TrySetAlternativeText(altText, page);
                    }
                }

                doc.Save(outputFile);
            }
        }
    }

    /// <summary>
    /// Program entry point required for a console application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method – currently a placeholder to satisfy the compiler.
        /// </summary>
        /// <param name="args">Command‑line arguments.</param>
        public static void Main(string[] args)
        {
            // No operation – the library methods can be invoked from external callers or unit tests.
        }
    }
}
