using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class XfdfMerger
{
    /// <summary>
    /// Merges multiple XFDF files into a single XFDF stream and imports the result into a PDF.
    /// </summary>
    /// <param name="xfdfFiles">Array of XFDF file paths to merge.</param>
    /// <param name="sourcePdf">Path to the source PDF that will receive the XFDF data.</param>
    /// <param name="outputPdf">Path where the resulting PDF will be saved.</param>
    public static void MergeAndImport(string[] xfdfFiles, string sourcePdf, string outputPdf)
    {
        // Validate inputs
        if (xfdfFiles == null || xfdfFiles.Length == 0)
            throw new ArgumentException("At least one XFDF file must be provided.", nameof(xfdfFiles));
        if (!File.Exists(sourcePdf))
            throw new FileNotFoundException("Source PDF not found.", sourcePdf);
        foreach (var xfdf in xfdfFiles)
        {
            if (!File.Exists(xfdf))
                throw new FileNotFoundException("XFDF file not found.", xfdf);
        }

        // Create a new XFDF document with the root element <xfdf>
        XDocument mergedXfdf = new XDocument(new XElement("xfdf"));

        // Merge the content of each XFDF file into the root
        foreach (var xfdfPath in xfdfFiles)
        {
            XDocument doc = XDocument.Load(xfdfPath);
            XElement root = doc.Root;
            if (root != null)
            {
                // Copy all child elements (e.g., <fields>, <annots>) into the merged document
                foreach (var element in root.Elements())
                {
                    // Clone the element to avoid modifying the source document
                    mergedXfdf.Root.Add(new XElement(element));
                }
            }
        }

        // Write the merged XFDF to a memory stream
        using (MemoryStream xfdfStream = new MemoryStream())
        {
            mergedXfdf.Save(xfdfStream);
            xfdfStream.Position = 0; // Reset stream position for reading

            // Use the Form facade to import the merged XFDF into the PDF
            // The Form constructor takes the source PDF and the desired output PDF paths
            using (Form form = new Form(sourcePdf, outputPdf))
            {
                form.ImportXfdf(xfdfStream);
                form.Save(); // Saves to the output PDF specified in the constructor
            }
        }
    }

    // Example usage
    static void Main()
    {
        string[] xfdfFiles = { "data1.xfdf", "data2.xfdf", "data3.xfdf" };
        string sourcePdf = "template.pdf";
        string outputPdf = "merged_output.pdf";

        try
        {
            MergeAndImport(xfdfFiles, sourcePdf, outputPdf);
            Console.WriteLine($"Merged XFDF imported successfully. Output saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}