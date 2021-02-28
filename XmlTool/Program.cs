//
// Program.cs
//
// Author:
//       Urist_McAurelian <Discord: Urist_McAurelian#2289>
//
// Copyright (c) 2021 Urist_McAurelian
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Globalization;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using CsvHelper;
using Microsoft.XmlDiffPatch;

namespace XmlTool {
	class Program {
		public const string header_comment =
			"This document was created with XmlTool, a tool originally developed for the Touhou Analepsia project by Urist McAurelian.\n\n" +
			"Feel free to check out our own project at https://forums.taleworlds.com/index.php?threads/tbd-bannerlord-mod-touhou-analepsia-illusory-nirvana.411674/ \n" +
			"and join our discord at https://discord.gg/PUqYrGZ \n\n" +
			"If you want to use this tool yourself for you own project, you can find it at https://github.com/RomanResistance/XmlTool";

		static void Main(string[] args) {
			Console.WriteLine("~~~NEW PROGRAM~~~");

			string root = AppDomain.CurrentDomain.BaseDirectory;
			string DSC = Path.DirectorySeparatorChar.ToString(); //For berevity's sake. 

			Directory.CreateDirectory(root + "Output");
			Directory.CreateDirectory(root + "Output" + DSC + "Languages");

			XmlWriterSettings localizationSettings = new XmlWriterSettings();
			localizationSettings.Indent = true;


			if (Directory.Exists(root + "Data" + DSC + "Clans")) {
				foreach (string file in Directory.GetFiles(root + "Data" + DSC + "Clans")) {
					if (!file.Split(".").Last().Equals("xml")) continue;
					//Console.WriteLine(file.Split(DSC).Last());
					Directory.CreateDirectory(root + "Output" + DSC + "CSVs" + DSC + "Clans");
					ClanConverter.XMLtoCSV(file, root + "Output" + DSC + "CSVs" + DSC + "Clans" + DSC + file.Split(DSC).Last().Split(".").First() + ".csv");
				}
			}

			if (Directory.Exists(root + "Data" + DSC + "SPCultures")) {
				foreach (string file in Directory.GetFiles(root + "Data" + DSC + "SPCultures")) {
					if (!file.Split(".").Last().Equals("xml")) continue;
					//Console.WriteLine(file.Split(DSC).Last());
					Directory.CreateDirectory(root + "Output" + DSC + "CSVs" + DSC + "SPCultures");
					CultureConverter.XMLtoCSV(file, root + "Output" + DSC + "CSVs" + DSC + "SPCultures" + DSC + file.Split(DSC).Last().Split(".").First() + ".csv");
				}
			}

			if (Directory.Exists(root + "Data" + DSC + "Heroes")) {
				foreach (string file in Directory.GetFiles(root + "Data" + DSC + "Heroes")) {
					if (!file.Split(".").Last().Equals("xml")) continue;
					//Console.WriteLine(file.Split(DSC).Last());
					Directory.CreateDirectory(root + "Output" + DSC + "CSVs" + DSC + "Heroes");
					HeroConverter.XMLtoCSV(file, root + "Output" + DSC + "CSVs" + DSC + "Heroes" + DSC + file.Split(DSC).Last().Split(".").First() + ".csv");
				}
			}

			if (Directory.Exists(root + "Data" + DSC + "Kingdoms")) {
				foreach (string file in Directory.GetFiles(root + "Data" + DSC + "Kingdoms")) {
					if (!file.Split(".").Last().Equals("xml")) continue;
					//Console.WriteLine(file.Split(DSC).Last());
					Directory.CreateDirectory(root + "Output" + DSC + "CSVs" + DSC + "Kingdoms");
					KingdomConverter.XMLtoCSV(file, root + "Output" + DSC + "CSVs" + DSC + "Kingdoms" + DSC + file.Split(DSC).Last().Split(".").First() + ".csv");
				}
			}

			if (Directory.Exists(root + "Data" + DSC + "NPCCharacters")) {
				foreach(string file in Directory.GetFiles(root + "Data" + DSC + "NPCCharacters")) {
					if (!file.Split(".").Last().Equals("xml")) continue;
					//Console.WriteLine(file.Split(DSC).Last());
					Directory.CreateDirectory(root + "Output" + DSC + "CSVs" + DSC + "NPCCharacters");
                    NPCCharacterConverter.XMLtoCSV(file, root + "Output" + DSC + "CSVs" + DSC + "NPCCharacters" + DSC + file.Split(DSC).Last().Split(".").First() + ".csv");
				}
			}

			if (Directory.Exists(root + "Data" + DSC + "partyTemplates")) {
				foreach (string file in Directory.GetFiles(root + "Data" + DSC + "partyTemplates")) {
					if (!file.Split(".").Last().Equals("xml")) continue;
					//Console.WriteLine(file.Split(DSC).Last());
					Directory.CreateDirectory(root + "Output" + DSC + "CSVs" + DSC + "partyTemplates");
					PartyTemplateConverter.XMLtoCSV(file, root + "Output" + DSC + "CSVs" + DSC + "partyTemplates" + DSC + file.Split(DSC).Last().Split(".").First() + ".csv");
				}
			}

			if (Directory.Exists(root + "Data" + DSC + "Settlements")) {
				foreach (string file in Directory.GetFiles(root + "Data" + DSC + "Settlements")) {
					if (!file.Split(".").Last().Equals("xml")) continue;
					//Console.WriteLine(file.Split(DSC).Last());
					Directory.CreateDirectory(root + "Output" + DSC + "CSVs" + DSC + "Settlements");
					SettlementConverter.XMLtoCSV(file, root + "Output" + DSC + "CSVs" + DSC + "Settlements" + DSC + file.Split(DSC).Last().Split(".").First() + ".csv");
				}
			}

			using (XmlWriter module_stringsWriter = XmlWriter.Create(root + "Output" + DSC + "module_strings.xml", localizationSettings)) {
				module_stringsWriter.WriteStartElement("base");
				module_stringsWriter.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
				module_stringsWriter.WriteAttributeString("xmlns", "xsd", null, "http://www.w3.org/2001/XMLSchema");
				module_stringsWriter.WriteAttributeString("type", "string");
				module_stringsWriter.WriteStartElement("strings");

				if (Directory.Exists(root + "Data" + DSC + "Clans")) { //TODO it works as it is, but could be better... Idealy support for multiple files in Data/Clans, regardless of whether that support is really needed.
					string clans_csv = null;
					foreach (string file in Directory.GetFiles(root + "Data" + DSC + "Clans")) {
						if (!file.Split(".").Last().Equals("csv")) continue;
						//Console.WriteLine(file.Split(DSC).Last().Split(".").First());

						Directory.CreateDirectory(root + "Output" + DSC + "Clans" + DSC + "Languages");
						clans_csv = file;
						break;
					}
					if (clans_csv != null) {
						using (XmlWriter localizationWriter = XmlWriter.Create(root + "Output" + DSC + "Clans" + DSC + "Languages" + DSC + "std_spclans_xml.xml", localizationSettings)) {
							initializeLocalizationWriter(localizationWriter);
							ClanConverter.CSVtoXML(clans_csv, root + "Output" + DSC + "Clans" + DSC + "spclans.xml", localizationWriter, module_stringsWriter);
							localizationWriter.WriteEndElement();
							localizationWriter.WriteEndElement();
						}
					}
				}
				if (Directory.Exists(root + "Data" + DSC + "SPCultures")) { //file.Split(DSC).Last().Split(".").First() + ".xml"
					foreach (string file in Directory.GetFiles(root + "Data" + DSC + "SPCultures")) {
						if (!file.Split(".").Last().Equals("csv")) continue;
						//Console.WriteLine(file.Split(DSC).Last().Split(".").First());

						Directory.CreateDirectory(root + "Output" + DSC + "SPCultures" + DSC + "Languages");
						using (XmlWriter localizationWriter = XmlWriter.Create(root + "Output" + DSC + "Languages" + DSC + "std_" + file.Split(DSC).Last().Split(".").First() + "_xml.xml", localizationSettings)) {
							initializeLocalizationWriter(localizationWriter);
							CultureConverter.CSVtoXML(file, root + "Output" + DSC + "SPCultures" + DSC + file.Split(DSC).Last().Split(".").First() + ".xml", localizationWriter, module_stringsWriter);
							localizationWriter.WriteEndElement();
							localizationWriter.WriteEndElement();
						}

					}
                }
				if (Directory.Exists(root + "Data" + DSC + "Heroes")) { //TODO it works as it is, but could be better... Idealy support for multiple files in Data/Heroes, regardless of whether that support is really needed.
					string heroes_csv = null;
					foreach(string file in Directory.GetFiles(root + "Data" + DSC + "Heroes")) {
						if (!file.Split(".").Last().Equals("csv")) continue;
						//Console.WriteLine(file.Split(DSC).Last().Split(".").First());

						Directory.CreateDirectory(root + "Output" + DSC + "Heroes" + DSC + "Languages");
						heroes_csv = file;
						break;
					}
					if (heroes_csv != null) {
						using (XmlWriter localizationWriter = XmlWriter.Create(root + "Output" + DSC + "Heroes" + DSC + "Languages" + DSC + "std_heroes_xml.xml", localizationSettings)) {
							initializeLocalizationWriter(localizationWriter);
							HeroConverter.CSVtoXML(heroes_csv, root + "Output" + DSC + "Heroes" + DSC + "heroes.xml", localizationWriter, module_stringsWriter);
							localizationWriter.WriteEndElement();
							localizationWriter.WriteEndElement();
						}
					}
				}
				if (Directory.Exists(root + "Data" + DSC + "Kingdoms")) { //TODO it works as it is, but could be better... Idealy support for multiple files in Data/Kingdoms, regardless of whether that support is really needed.
					string kingdoms_csv = null;
					foreach (string file in Directory.GetFiles(root + "Data" + DSC + "Kingdoms")) {
						if (!file.Split(".").Last().Equals("csv")) continue;
						//Console.WriteLine(file.Split(DSC).Last().Split(".").First());

						Directory.CreateDirectory(root + "Output" + DSC + "Kingdoms" + DSC + "Languages");
						kingdoms_csv = file;
						break;
					}
					if (kingdoms_csv != null) {
						using (XmlWriter localizationWriter = XmlWriter.Create(root + "Output" + DSC + "Kingdoms" + DSC + "Languages" + DSC + "std_spkingdoms_xml.xml", localizationSettings)) {
							initializeLocalizationWriter(localizationWriter);
							KingdomConverter.CSVtoXML(kingdoms_csv, root + "Output" + DSC + "Kingdoms" + DSC + "spkingdoms.xml", localizationWriter, module_stringsWriter);
							localizationWriter.WriteEndElement();
							localizationWriter.WriteEndElement();
						}
					}
				}
				if (Directory.Exists(root + "Data" + DSC + "NPCCharacters")) {
					foreach (string file in Directory.GetFiles(root + "Data" + DSC + "NPCCharacters")) {
						if (!file.Split(".").Last().Equals("csv")) continue;
						//Console.WriteLine(file.Split(DSC).Last().Split(".").First());

						Directory.CreateDirectory(root + "Output" + DSC + "NPCCharacters" + DSC + "Languages");
						using (XmlWriter localizationWriter = XmlWriter.Create(root + "Output" + DSC + "NPCCharacters" + DSC + "Languages" + DSC + "std_" + file.Split(DSC).Last().Split(".").First() + "_xml.xml", localizationSettings)) {
							initializeLocalizationWriter(localizationWriter);
							NPCCharacterConverter.CSVtoXML(file, root + "Output" + DSC + "NPCCharacters" + DSC + file.Split(DSC).Last().Split(".").First() + ".xml", localizationWriter, module_stringsWriter);
							localizationWriter.WriteEndElement();
							localizationWriter.WriteEndElement();
						}
					}
				}
				if (Directory.Exists(root + "Data" + DSC + "partyTemplates")) {
					foreach (string file in Directory.GetFiles(root + "Data" + DSC + "partyTemplates")) {
						if (!file.Split(".").Last().Equals("csv")) continue;
						//Console.WriteLine(file.Split(DSC).Last().Split(".").First());

						Directory.CreateDirectory(root + "Output" + DSC + "partyTemplates");
						PartyTemplateConverter.CSVtoXML(file, root + "Output" + DSC + "partyTemplates" + DSC + file.Split(DSC).Last().Split(".").First() + ".xml");
					}
				}
				if (Directory.Exists(root + "Data" + DSC + "Settlements")) {
					string csv_file = null, xscene_file = null;

					foreach (string file in Directory.GetFiles(root + "Data" + DSC + "Settlements")) {
						switch(file.Split(".").Last()) {
							case "csv":
								if (csv_file == null) csv_file = file;
								else Console.WriteLine("Warning! There exists more than one csv in \"" + root + "Data" + DSC + "Settlements\", this is unsuported and unexpected behaviour may occur!");
								break;
							case "xscene":
								if (xscene_file == null) xscene_file = file;
								else Console.WriteLine("Warning! There exists more than one xscene in \"" + root + "Data" + DSC + "Settlements\", this is unsuported and unexpected behaviour may occur!");
								break;
							default:
								continue;
                        }
					}
					if (csv_file != null && xscene_file != null) {
						Directory.CreateDirectory(root + "Output" + DSC + "Settlements");

						using (XmlWriter localizationWriter = XmlWriter.Create(root + "Output" + DSC + "Languages" + DSC + "std_settlements_xml.xml", localizationSettings)) {
							initializeLocalizationWriter(localizationWriter);
							SettlementConverter.CSVtoXML(csv_file, root + "Output" + DSC + "Settlements" + DSC + csv_file.Split(DSC).Last(), root + "Output" + DSC + "Settlements" + DSC + csv_file.Split(DSC).Last().Split(".").First() + ".xml", xscene_file, root + "Output" + DSC + "Settlements" + DSC + xscene_file.Split(DSC).Last().Split(".").First() + ".xscene", localizationWriter, module_stringsWriter);
							localizationWriter.WriteEndElement();
							localizationWriter.WriteEndElement();
						}
					} else if (csv_file == null && xscene_file == null) {
					} else Console.WriteLine("You are missing either a xscene file and/or a csv file in \"" + root + "Data" + DSC + "Settlements\", to enable csv to xml conversion, please ensure both a xscene and csv file are present.");
                }

				module_stringsWriter.WriteEndElement();
				module_stringsWriter.WriteEndElement();
			}

			//Damnable MacOS...
			IEnumerable<string> filenames = Directory.EnumerateFiles(root, ".DS_STORE", SearchOption.AllDirectories);
			foreach(string f in filenames) {
				File.Delete(f);
            }

			Directory.CreateDirectory(root + "Differences/NPCCharacters");

			Console.WriteLine(XMLfilesIdentical(root + "Data/SPCultures/spcultures.xml", root + "Output/SPCultures/spcultures.xml", root + "Differences" + DSC + "diff_spcultures.xml"));
			Console.WriteLine(XMLfilesIdentical(root + "Data/Clans/spclans.xml", root + "Output/Clans/spclans.xml", root + "Differences" + DSC + "diff_spclans.xml"));
			Console.WriteLine(XMLfilesIdentical(root + "Data/Kingdoms/spkingdoms.xml", root + "Output/Kingdoms/spkingdoms.xml", root + "Differences" + DSC + "diff_spkingdoms.xml"));

			foreach (string file in Directory.EnumerateFiles(root + "Data" + DSC + "NPCCharacters").Select(Path.GetFileName)) {
				string f1 = root + "Data" + DSC + "NPCCharacters" + DSC + file, f2 = root + "Output" + DSC + "NPCCharacters" + DSC + file;
				if (File.Exists(f1) && File.Exists(f2)) {
					Console.WriteLine(XMLfilesIdentical(f1, f2, root + "Differences" + DSC + "NPCCharacters" + DSC + "diff_" + file));
				}
            }

			Console.WriteLine(XMLfilesIdentical(root + "Data/Settlements/settlements.xml", root + "Output/Settlements/settlements.xml", root + "Differences" + DSC + "Settlements" + DSC + "diff_settlements.xml"));

			Console.WriteLine("~~~Info~~~");
			if(NPCCharacterConverter.NeededEquipmentSets!=-1) Console.WriteLine("Up to {0} Equipment Sets on {2} NPCCharacters were trimmed. Please yell at Urist_McAurelian#2289 on Discord that you need support for more Equipment Sets. \n At the moment, the converter only supports having {1} equipment sets.", NPCCharacterConverter.NeededEquipmentSets-NPCCharacterConverter.AllowedEquipmentSets, NPCCharacterConverter.AllowedEquipmentSets, NPCCharacterConverter.AffectedNPCCharacters);
		}

		public static string TrimB(string s) {
			if (s == null) return null;
			return s.Split("}").Last();
		}
		public static string TrimD(string s) {
			if (s == null) return null;
			return s.Split(".").Last();
		}

		[Obsolete("Use GetLocalizedString instead")]
		public static void writeLocalizationNode(XmlWriter writer, string id, string text) {
			writer.WriteStartElement("string");
			writer.WriteAttributeString("id", id);
			writer.WriteAttributeString("text", text);
			writer.WriteEndElement();
		}

		private static List<string> localized_strings = new List<string>();

		// Returns the localized string with it's id.
		public static string GetLocalizedString(XmlWriter writer, string text, string id, string key = null, string converter_type = null) {
			id = id?.Replace(" ", "_");

			string tag = id;
			if (id == null || id.Equals("")) return "{=!}" + text;

			if (key != null) tag = key + "." + tag;
			if (converter_type != null) tag = converter_type + "." + tag;

			if (!localized_strings.Contains(tag)) {
				localized_strings.Add(tag);

				writer.WriteStartElement("string");
				writer.WriteAttributeString("id", tag);
				writer.WriteAttributeString("text", text);
				writer.WriteEndElement();
			} //else Console.WriteLine("The localization '{0}' has been written more than once.", tag);

			return "{=" + tag + "}" + text;
		}

		public static void initializeLocalizationWriter(XmlWriter localizationWriter) {
			localizationWriter.WriteStartElement("base");
			localizationWriter.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
			localizationWriter.WriteAttributeString("xmlns", "xsd", null, "http://www.w3.org/2001/XMLSchema");
			localizationWriter.WriteAttributeString("type", "string");
			localizationWriter.WriteStartElement("tags");
			localizationWriter.WriteStartElement("tag");
			localizationWriter.WriteAttributeString("language", "English");
			localizationWriter.WriteEndElement();
			localizationWriter.WriteEndElement();
			localizationWriter.WriteStartElement("strings");
		}

		public static void writeHeadderComment(XmlWriter writter) {
			writter.WriteComment(header_comment);
        }

		public static bool XMLfilesIdentical(string originalFile, string finalFile, string diff) {
			XmlWriterSettings settings = new XmlWriterSettings() {
				Indent = true,
				IndentChars = "\t"
			};

			XmlDiff xmlDiff = new XmlDiff();
			XmlReader original = XmlReader.Create(originalFile);
			XmlReader final = XmlReader.Create(finalFile);

			Directory.CreateDirectory(diff.Replace(Path.GetFileName(diff), ""));
			XmlWriter writer = XmlWriter.Create(diff, settings);

			xmlDiff.Options = XmlDiffOptions.IgnorePI |
							  XmlDiffOptions.IgnoreChildOrder |
							  XmlDiffOptions.IgnoreComments |
							  XmlDiffOptions.IgnoreWhitespace |
							  XmlDiffOptions.IgnoreXmlDecl;

			return xmlDiff.Compare(original, final, writer);
		}
	}
}
