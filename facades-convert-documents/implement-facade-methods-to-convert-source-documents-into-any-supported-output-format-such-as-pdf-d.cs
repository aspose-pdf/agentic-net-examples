using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    public static class DocumentConverterFacade
    {
        /// <summary>
        /// Converts a source document to the format implied by the destination file extension.
        /// Supported source formats: PDF, HTML, DOC/DOCX, PPT/PPTX, XLS/XLSX, CGM, SVG, PS, PCL.
        /// Supported destination formats: PDF, DOCX, XLSX, HTML, SVG, XML.
        /// </summary>
        /// <param name="sourcePath">Full path to the source file.</param>
        /// <param name="destinationPath">Full path to the desired output file.</param>
        public static void Convert(string sourcePath, string destinationPath)
        {
            if (string.IsNullOrWhiteSpace(sourcePath))
                throw new ArgumentException("Source path must be provided.", nameof(sourcePath));

            if (string.IsNullOrWhiteSpace(destinationPath))
                throw new ArgumentException("Destination path must be provided.", nameof(destinationPath));

            if (!File.Exists(sourcePath))
                throw new FileNotFoundException($"Source file not found: {sourcePath}");

            // Determine save options based on destination extension
            SaveOptions saveOptions = GetSaveOptionsForExtension(Path.GetExtension(destinationPath));

            // Load the source document directly – Aspose.Pdf auto‑detects the format.
            using (Document doc = new Document(sourcePath))
            {
                // Save the document. For PDF output, saveOptions will be null and Save(string) is used.
                if (saveOptions != null)
                    doc.Save(destinationPath, saveOptions);
                else
                    doc.Save(destinationPath);
            }
        }

        /// <summary>
        /// Returns a suitable SaveOptions instance for the given file extension.
        /// Returns null for PDF (default Save without options).
        /// </summary>
        private static SaveOptions GetSaveOptionsForExtension(string extension)
        {
            if (string.IsNullOrWhiteSpace(extension))
                return null;

            switch (extension.ToLowerInvariant())
            {
                case ".docx":
                    return new DocSaveOptions { Format = DocSaveOptions.DocFormat.DocX };

                case ".xlsx":
                    return new ExcelSaveOptions { Format = ExcelSaveOptions.ExcelFormat.XLSX };

                case ".html":
                case ".htm":
                    return new HtmlSaveOptions();

                case ".svg":
                    return new SvgSaveOptions();

                case ".xml":
                    return new XmlSaveOptions();

                // PDF does not require explicit SaveOptions
                case ".pdf":
                    return null;

                default:
                    // For any other format, return null and let Save(string) attempt default behavior
                    return null;
            }
        }
    }

    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    internal class Program
    {
        private static void Main()
        {
            // No operation – the library is intended to be used via DocumentConverterFacade.
        }
    }
}
