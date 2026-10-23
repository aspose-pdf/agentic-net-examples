using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

// -----------------------------------------------------------------------------
// Minimal wrapper for PdfSignatureEditor when the real class is not available.
// This stub uses Aspose.Pdf.Document to remove all signature fields from a PDF.
// -----------------------------------------------------------------------------
namespace Aspose.Pdf.Facades
{
    /// <summary>
    /// Simple replacement for the missing Aspose.Pdf.Facades.PdfSignatureEditor class.
    /// It provides the subset of functionality required by the script: BindPdf,
    /// RemoveAllSignatures and Save.
    /// </summary>
    public class PdfSignatureEditor
    {
        private Document? _document;

        /// <summary>
        /// Loads the PDF file into an Aspose.Pdf.Document instance.
        /// </summary>
        /// <param name="path">Full path to the PDF file.</param>
        public void BindPdf(string path)
        {
            _document = new Document(path);
        }

        /// <summary>
        /// Removes every signature field present in the document.
        /// The real Aspose implementation offers a dedicated API; this stub
        /// achieves the same effect by deleting all signature fields from the
        /// Form.Fields collection.
        /// </summary>
        public void RemoveAllSignatures()
        {
            if (_document?.Form?.Fields == null)
                return;

            // Collect the names of all signature fields first to avoid modifying the collection while iterating.
            var signatureFieldNames = new List<string>();
            foreach (var field in _document.Form.Fields)
            {
                if (field is SignatureField)
                    signatureFieldNames.Add(field.Name);
            }

            // Delete each signature field by name using the correct Form API.
            foreach (var name in signatureFieldNames)
            {
                _document.Form.Delete(name);
            }
        }

        /// <summary>
        /// Saves the (now unsigned) PDF back to the specified path.
        /// </summary>
        /// <param name="path">Destination file path.</param>
        public void Save(string path)
        {
            _document?.Save(path);
        }
    }
}

class RemoveAllSignatures
{
    static void Main(string[] args)
    {
        // Expect a directory path as the first argument; if not provided, use the current directory.
        string rootDir = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

        if (!Directory.Exists(rootDir))
        {
            Console.Error.WriteLine($"Directory not found: {rootDir}");
            return;
        }

        // Recursively process all PDF files.
        foreach (string pdfPath in Directory.EnumerateFiles(rootDir, "*.pdf", SearchOption.AllDirectories))
        {
            try
            {
                // The stub class implements the same API as the original Aspose editor.
                var signatureEditor = new PdfSignatureEditor();

                // Load the PDF.
                signatureEditor.BindPdf(pdfPath);

                // Remove every signature present in the document.
                signatureEditor.RemoveAllSignatures();

                // Overwrite the original file with the unsigned version.
                signatureEditor.Save(pdfPath);

                Console.WriteLine($"Signatures removed: {pdfPath}");
            }
            catch (Exception ex)
            {
                // Log any errors but continue processing other files.
                Console.Error.WriteLine($"Failed to process '{pdfPath}': {ex.Message}");
            }
        }
    }
}
