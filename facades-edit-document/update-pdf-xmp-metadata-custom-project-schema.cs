using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_xmp.pdf";

        // Custom metadata values
        const string projectId = "MyProject";
        const string version   = "1.2.3";

        // -----------------------------------------------------------------
        // Ensure a source PDF exists – create a minimal placeholder if it does not.
        // -----------------------------------------------------------------
        if (!File.Exists(inputPdf))
        {
            using var placeholder = new Document();
            placeholder.Pages.Add();
            placeholder.Save(inputPdf);
        }

        // Load the source PDF, add custom XMP metadata, and save a copy.
        // Document must be wrapped in a using block (lifecycle rule).
        using (Document doc = new Document(inputPdf))
        {
            // Register a custom namespace for the project schema.
            const string projPrefix    = "proj";
            const string projNamespace = "http://example.com/ns/project/1.0/";
            doc.Metadata.RegisterNamespaceUri(projPrefix, projNamespace);

            // Set the custom XMP properties. The "xmp:" prefix is required for
            // the metadata indexer, followed by the custom namespace prefix.
            doc.Metadata[$"xmp:{projPrefix}:ProjectIdentifier"] = projectId;
            doc.Metadata[$"xmp:{projPrefix}:VersionNumber"]   = version;

            // Save the PDF with the updated XMP packet.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Custom XMP metadata added to '{outputPdf}'.");
    }
}