using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_rich.pdf";
        const string fieldName = "RichTextField";
        const string htmlContent = "<p>This is <b>bold</b>, <i>italic</i> and <u>underlined</u> text.</p>";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Retrieve the field from the form's Fields collection
                TextBoxField tb = doc.Form?.Fields?
                                      .FirstOrDefault(f => f.Name == fieldName) as TextBoxField;

                if (tb != null)
                {
                    // Try to enable rich‑text mode via reflection – works on newer versions
                    Type tbType = tb.GetType();
                    PropertyInfo isRichProp = tbType.GetProperty("IsRichText");
                    PropertyInfo richProp = tbType.GetProperty("RichText");

                    if (isRichProp != null && richProp != null)
                    {
                        // Set IsRichText = true
                        isRichProp.SetValue(tb, true);

                        // Create a RichTextFragment instance via reflection if the type exists
                        Type rtfType = Type.GetType("Aspose.Pdf.Text.RichTextFragment, Aspose.Pdf");
                        if (rtfType != null)
                        {
                            // ctor: RichTextFragment(string html)
                            object rtfInstance = Activator.CreateInstance(rtfType, htmlContent);
                            richProp.SetValue(tb, rtfInstance);
                        }
                        else
                        {
                            // Fallback – assign the raw HTML string directly
                            richProp.SetValue(tb, htmlContent);
                        }

                        // Optional: clear the plain value to avoid fallback rendering
                        tb.Value = string.Empty;
                    }
                    else
                    {
                        // The current Aspose.Pdf version does not expose rich‑text API – store as plain text
                        tb.Value = htmlContent;
                    }
                }
                else
                {
                    Console.Error.WriteLine($"Form field '{fieldName}' not found or is not a TextBoxField.");
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Rich text field saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
