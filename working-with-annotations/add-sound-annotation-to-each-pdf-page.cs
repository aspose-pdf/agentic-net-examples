using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_with_sound.pdf";
        const string soundFile = "tone.wav";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(soundFile))
        {
            Console.Error.WriteLine($"Sound file not found: {soundFile}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPdf))
            {
                // Try to obtain the SoundAnnotation type from the loaded Aspose.Pdf assembly.
                Type soundAnnoType = Type.GetType("Aspose.Pdf.Annotations.SoundAnnotation, Aspose.Pdf");
                Type soundType = Type.GetType("Aspose.Pdf.Annotations.Sound, Aspose.Pdf");

                // If SoundAnnotation is not present (older Aspose.Pdf versions) fall back to RichMediaAnnotation.
                bool useRichMedia = soundAnnoType == null || soundType == null;
                Type richMediaAnnoType = null;
                Type richMediaContentType = null;
                if (useRichMedia)
                {
                    richMediaAnnoType = Type.GetType("Aspose.Pdf.Annotations.RichMediaAnnotation, Aspose.Pdf");
                    richMediaContentType = Type.GetType("Aspose.Pdf.Annotations.RichMediaContent, Aspose.Pdf");
                }

                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];
                    // Invisible rectangle – the annotation will not be visible on the page.
                    var rect = new Rectangle(0, 0, 0, 0);

                    if (!useRichMedia)
                    {
                        // ----- SoundAnnotation path (available in newer versions) -----
                        // Create instance using the (Page, Rectangle) constructor via reflection.
                        object soundAnno = Activator.CreateInstance(soundAnnoType, page, rect);

                        // Load the WAV file into a Sound object (also via reflection).
                        using (FileStream soundStream = File.OpenRead(soundFile))
                        {
                            object soundObj = Activator.CreateInstance(soundType, soundStream);
                            // Set the Sound property.
                            PropertyInfo soundProp = soundAnnoType.GetProperty("Sound");
                            soundProp?.SetValue(soundAnno, soundObj);
                        }

                        // Set the annotation to play automatically when the page is opened.
                        PropertyInfo isOpenProp = soundAnnoType.GetProperty("IsOpen");
                        isOpenProp?.SetValue(soundAnno, true);

                        // Add the annotation to the page.
                        page.Annotations.Add((Annotation)soundAnno);
                    }
                    else if (richMediaAnnoType != null && richMediaContentType != null)
                    {
                        // ----- RichMediaAnnotation fallback (older versions) -----
                        object richMediaAnno = Activator.CreateInstance(richMediaAnnoType, page, rect);

                        // Create RichMediaContent and attach the audio file.
                        object richMediaContent = Activator.CreateInstance(richMediaContentType);
                        MethodInfo addFileMethod = richMediaContentType.GetMethod("AddFile", new[] { typeof(string) });
                        addFileMethod?.Invoke(richMediaContent, new object[] { soundFile });

                        // Assign the content to the annotation.
                        PropertyInfo contentProp = richMediaAnnoType.GetProperty("RichMediaContent");
                        contentProp?.SetValue(richMediaAnno, richMediaContent);

                        // Try to enable auto‑play on page open (if the property exists).
                        PropertyInfo activationProp = richMediaAnnoType.GetProperty("Activation");
                        if (activationProp != null)
                        {
                            object activationObj = activationProp.GetValue(richMediaAnno);
                            if (activationObj != null)
                            {
                                PropertyInfo playOnOpenProp = activationObj.GetType().GetProperty("IsPlayOnPageOpen");
                                playOnOpenProp?.SetValue(activationObj, true);
                            }
                        }

                        page.Annotations.Add((Annotation)richMediaAnno);
                    }
                    else
                    {
                        // If neither annotation type is available, write a warning and continue.
                        Console.Error.WriteLine("Neither SoundAnnotation nor RichMediaAnnotation is available in the current Aspose.Pdf version.");
                        break;
                    }
                }

                doc.Save(outputPdf);
            }

            Console.WriteLine($"PDF with sound annotations saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
