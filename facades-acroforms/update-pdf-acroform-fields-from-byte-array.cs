using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

namespace AsposePdfApi
{
    public static class PdfFormHelper
    {
        /// <summary>
        /// Loads a PDF from a byte array, updates its form fields, and returns the modified PDF as a byte array.
        /// </summary>
        /// <param name="pdfBytes">The original PDF content.</param>
        /// <returns>The updated PDF content.</returns>
        public static byte[] UpdatePdfFormFields(byte[] pdfBytes)
        {
            if (pdfBytes == null || pdfBytes.Length == 0)
                throw new ArgumentException("PDF byte array is null or empty.", nameof(pdfBytes));

            // Load the PDF from the input byte array using a MemoryStream.
            using (MemoryStream inputStream = new MemoryStream(pdfBytes))
            using (Document document = new Document(inputStream)) // document‑disposal‑with‑using rule
            {
                var form = document.Form;
                if (form != null)
                {
                    // ----- Text field -----
                    if (form["Name"] is TextBoxField txtField)
                    {
                        txtField.Value = "John Doe";
                    }

                    // ----- Check box -----
                    if (form["Subscribe"] is CheckboxField chkField)
                    {
                        chkField.Checked = true; // true = checked, false = unchecked
                    }

                    // ----- Radio button -----
                    // The radio button group is represented by a RadioButtonField.
                    // We locate the option whose name matches the desired value ("Male")
                    // and set the group's Selected property (1‑based index).
                    if (form["Gender"] is RadioButtonField radioField)
                    {
                        int selectedIndex = 0; // 0 means not found
                        int idx = 1; // Aspose uses 1‑based indices for Selected
                        foreach (Option opt in radioField.Options)
                        {
                            if (opt.Name.Equals("Male", StringComparison.OrdinalIgnoreCase))
                            {
                                selectedIndex = idx;
                                break;
                            }
                            idx++;
                        }
                        if (selectedIndex > 0)
                        {
                            radioField.Selected = selectedIndex;
                        }
                    }
                }

                // Save the modified PDF into a new MemoryStream.
                using (MemoryStream outputStream = new MemoryStream())
                {
                    document.Save(outputStream);
                    return outputStream.ToArray();
                }
            }
        }
    }

    // Simple console entry point to make the project buildable.
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: AsposePdfApi <inputPdfPath> <outputPdfPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"Input file not found: {inputPath}");
                return;
            }

            byte[] originalPdf = File.ReadAllBytes(inputPath);
            byte[] updatedPdf = PdfFormHelper.UpdatePdfFormFields(originalPdf);
            File.WriteAllBytes(outputPath, updatedPdf);
            Console.WriteLine($"Updated PDF saved to: {outputPath}");
        }
    }
}
