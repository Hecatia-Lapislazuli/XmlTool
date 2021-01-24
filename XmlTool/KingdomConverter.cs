//
// KingdomConverter.cs
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
using static XmlTool.Program;

namespace XmlTool {
	//Functionally same result after running through twice.
	public class KingdomConverter {
		public static void Kingdoms_CSVtoXML(string fileInput, string fileOutput, XmlWriter localizationWriter, XmlWriter module_strings_writer) {
			StreamReader reader = new StreamReader(fileInput);
			CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture);

			KingdomRecord kingdomsRecord = new KingdomRecord();
			IEnumerable<KingdomRecord> records = csv.EnumerateRecords(kingdomsRecord);

			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;

			using (XmlWriter writer = XmlWriter.Create(fileOutput, settings)) {
				writer.WriteStartElement("Kingdoms");

				writeHeadderComment(writer);

				foreach (KingdomRecord record in records) {
					if (record.id.Equals("TODO")) break;
					if (record.id.Equals("")) continue;

					writer.WriteStartElement("Kingdom");

					//Changes

					//Defaults

					//Temporary

					//Write
					writer.WriteAttributeString("id", record.id);
					if (!record.banner_key.Equals("")) writer.WriteAttributeString("banner_key", record.banner_key);
					if (!record.owner.Equals("")) writer.WriteAttributeString("owner", "Hero." + record.owner);
					if (!record.label_color.Equals("")) writer.WriteAttributeString("label_color", record.label_color);
					writer.WriteAttributeString("primary_banner_color", record.primary_banner_color);
					writer.WriteAttributeString("secondary_banner_color", record.secondary_banner_color);
					if (!record.color.Equals("")) writer.WriteAttributeString("color", record.color);
					if (!record.color2.Equals("")) writer.WriteAttributeString("color2", record.color2);
					if (!record.alternative_color.Equals("")) writer.WriteAttributeString("alternative_color", record.alternative_color);
					if (!record.alternative_color2.Equals("")) writer.WriteAttributeString("alternative_color2", record.alternative_color2);
					writer.WriteAttributeString("culture", "Culture." + record.culture);
					if (!record.settlement_banner_mesh.Equals("")) writer.WriteAttributeString("settlement_banner_mesh", record.settlement_banner_mesh);
					if (!record.flag_mesh.Equals("")) writer.WriteAttributeString("flag_mesh", record.flag_mesh);

					if (!record.name.Equals("")) writer.WriteAttributeString("name", GetLocalizedString(localizationWriter, record.name, record.id, "name", "Kingdoms.Kingdom"));
					if (!record.short_name.Equals("")) writer.WriteAttributeString("short_name", GetLocalizedString(localizationWriter, record.short_name, record.id, "short_name", "Kingdoms.Kingdom"));
					if (!record.text.Equals("")) writer.WriteAttributeString("text", GetLocalizedString(localizationWriter, record.text, record.id, "text", "Kingdoms.Kingdom"));
					if (!record.title.Equals("")) writer.WriteAttributeString("title", GetLocalizedString(localizationWriter, record.title, record.id, "title", "Kingdoms.Kingdom"));
					if (!record.ruler_title.Equals("")) writer.WriteAttributeString("ruler_title", GetLocalizedString(localizationWriter, record.ruler_title, record.id, "ruler_title", "Kingdoms.Kingdom"));
					
					writer.WriteStartElement("relationships");
					if (!record.Relationships_Kingdom0_name.Equals("")) {
						writer.WriteStartElement("relationship");

						writer.WriteAttributeString("kingdom", "Kingdom." + record.Relationships_Kingdom0_name);
						writer.WriteAttributeString("value", record.Relationships_Kingdom0_value);
						if (!record.Relationships_Kingdom0_isAtWar.Equals("")) writer.WriteAttributeString("isAtWar", record.Relationships_Kingdom0_isAtWar.ToLower());

						writer.WriteEndElement();
					}
					if (!record.Relationships_Kingdom1_name.Equals("")) {
						writer.WriteStartElement("relationship");

						writer.WriteAttributeString("kingdom", "Kingdom." + record.Relationships_Kingdom1_name);
						writer.WriteAttributeString("value", record.Relationships_Kingdom1_value);
						if (!record.Relationships_Kingdom1_isAtWar.Equals("")) writer.WriteAttributeString("isAtWar", record.Relationships_Kingdom1_isAtWar.ToLower());

						writer.WriteEndElement();
					}
					if (!record.Relationships_Kingdom2_name.Equals("")) {
						writer.WriteStartElement("relationship");

						writer.WriteAttributeString("kingdom", "Kingdom." + record.Relationships_Kingdom2_name);
						writer.WriteAttributeString("value", record.Relationships_Kingdom2_value);
						if (!record.Relationships_Kingdom2_isAtWar.Equals("")) writer.WriteAttributeString("isAtWar", record.Relationships_Kingdom2_isAtWar.ToLower());

						writer.WriteEndElement();
					}
					if (!record.Relationships_Kingdom3_name.Equals("")) {
						writer.WriteStartElement("relationship");

						writer.WriteAttributeString("kingdom", "Kingdom." + record.Relationships_Kingdom3_name);
						writer.WriteAttributeString("value", record.Relationships_Kingdom3_value);
						if (!record.Relationships_Kingdom3_isAtWar.Equals("")) writer.WriteAttributeString("isAtWar", record.Relationships_Kingdom3_isAtWar.ToLower());

						writer.WriteEndElement();
					}
					//~~~
					if (!record.Relationships_Clan0_name.Equals("")) {
						writer.WriteStartElement("relationship");

						writer.WriteAttributeString("clan", "Faction." + record.Relationships_Clan0_name);
						writer.WriteAttributeString("value", record.Relationships_Clan0_value);
						if (!record.Relationships_Clan0_isAtWar.Equals("")) writer.WriteAttributeString("isAtWar", record.Relationships_Clan0_isAtWar.ToLower());

						writer.WriteEndElement();
					}
					if (!record.Relationships_Clan1_name.Equals("")) {
						writer.WriteStartElement("relationship");

						writer.WriteAttributeString("clan", "Faction." + record.Relationships_Clan1_name);
						writer.WriteAttributeString("value", record.Relationships_Clan1_value);
						if (!record.Relationships_Clan1_isAtWar.Equals("")) writer.WriteAttributeString("isAtWar", record.Relationships_Clan1_isAtWar.ToLower());

						writer.WriteEndElement();
					}
					if (!record.Relationships_Clan2_name.Equals("")) {
						writer.WriteStartElement("relationship");

						writer.WriteAttributeString("clan", "Faction." + record.Relationships_Clan2_name);
						writer.WriteAttributeString("value", record.Relationships_Clan2_value);
						if (!record.Relationships_Clan2_isAtWar.Equals("")) writer.WriteAttributeString("isAtWar", record.Relationships_Clan2_isAtWar.ToLower());

						writer.WriteEndElement();
					}
					if (!record.Relationships_Clan3_name.Equals("")) {
						writer.WriteStartElement("relationship");

						writer.WriteAttributeString("clan", "Faction." + record.Relationships_Clan3_name);
						writer.WriteAttributeString("value", record.Relationships_Clan3_value);
						if (!record.Relationships_Clan3_isAtWar.Equals("")) writer.WriteAttributeString("isAtWar", record.Relationships_Clan3_isAtWar.ToLower());

						writer.WriteEndElement();
					}
					writer.WriteEndElement();

					writer.WriteStartElement("policies");
					if (!record.Policy0.Equals("") || !record.Policy1.Equals("") || !record.Policy2.Equals("") || !record.Policy3.Equals("")) {
						if (!record.Policy0.Equals("")) {
							writer.WriteStartElement("policy");
							writer.WriteAttributeString("id", record.Policy0);
							writer.WriteEndElement();
						}
						if (!record.Policy1.Equals("")) {
							writer.WriteStartElement("policy");
							writer.WriteAttributeString("id", record.Policy1);
							writer.WriteEndElement();
						}
						if (!record.Policy2.Equals("")) {
							writer.WriteStartElement("policy");
							writer.WriteAttributeString("id", record.Policy2);
							writer.WriteEndElement();
						}
						if (!record.Policy3.Equals("")) {
							writer.WriteStartElement("policy");
							writer.WriteAttributeString("id", record.Policy3);
							writer.WriteEndElement();
						}
					}
					writer.WriteEndElement();

					writer.WriteEndElement();
				}
				writer.WriteEndElement();
			}
		}
		public static void Kingdoms_XMLtoCSV(string xmlInput, string csvOutput) {
			List<KingdomRecord> records = new List<KingdomRecord>();

			using (XmlReader root = XmlReader.Create(xmlInput)) {
				root.MoveToContent();
				while (root.Read()) {
					if (root.NodeType.Equals(XmlNodeType.Element)) break;
				}

				while (!root.EOF) {
					if (root.NodeType != XmlNodeType.Element) {
						root.Read();
						continue;
					}
					KingdomRecord record = new KingdomRecord();

					record.id = root.GetAttribute("id");
					record.banner_key = root.GetAttribute("banner_key");
					record.owner = TrimD(root.GetAttribute("owner"));
					record.label_color = root.GetAttribute("label_color");
					record.primary_banner_color = root.GetAttribute("primary_banner_color");
					record.secondary_banner_color = root.GetAttribute("secondary_banner_color");
					record.color = root.GetAttribute("color");
					record.color2 = root.GetAttribute("color2");
					record.alternative_color = root.GetAttribute("alternative_color");
					record.alternative_color2 = root.GetAttribute("alternative_color2");
					record.culture = TrimD(root.GetAttribute("culture"));
					record.settlement_banner_mesh = root.GetAttribute("settlement_banner_mesh");
					record.flag_mesh = root.GetAttribute("flag_mesh");
					record.name = TrimB(root.GetAttribute("name"));
					record.short_name = TrimB(root.GetAttribute("short_name"));
					record.text = TrimB(root.GetAttribute("text"));
					record.title = TrimB(root.GetAttribute("title"));
					record.ruler_title = TrimB(root.GetAttribute("ruler_title"));

					XmlReader sub = root.ReadSubtree();
					sub.Read();

					int c1=0, c2=0, c3=0;

					while(!sub.EOF) {
						switch(sub.Name) {
							case "Kingdom":
								break;
							case "relationships":
								XmlReader relationshipsReader = sub.ReadSubtree();
								while (relationshipsReader.Read()) {
									if (!relationshipsReader.NodeType.Equals(XmlNodeType.Element) || relationshipsReader.Name.Equals("relationships")) continue;
									if (relationshipsReader.GetAttribute("kingdom") != null) {
										switch(c1) {
											case 0:
												record.Relationships_Kingdom0_name = TrimD(relationshipsReader.GetAttribute("kingdom"));
												record.Relationships_Kingdom0_value = relationshipsReader.GetAttribute("value");
												record.Relationships_Kingdom0_isAtWar = relationshipsReader.GetAttribute("isAtWar");
												break;
											case 1:
												record.Relationships_Kingdom1_name = TrimD(relationshipsReader.GetAttribute("kingdom"));
												record.Relationships_Kingdom1_value = relationshipsReader.GetAttribute("value");
												record.Relationships_Kingdom1_isAtWar = relationshipsReader.GetAttribute("isAtWar");
												break;
											case 2:
												record.Relationships_Kingdom2_name = TrimD(relationshipsReader.GetAttribute("kingdom"));
												record.Relationships_Kingdom2_value = relationshipsReader.GetAttribute("value");
												record.Relationships_Kingdom2_isAtWar = relationshipsReader.GetAttribute("isAtWar");
												break;
											case 3:
												record.Relationships_Kingdom3_name = TrimD(relationshipsReader.GetAttribute("kingdom"));
												record.Relationships_Kingdom3_value = relationshipsReader.GetAttribute("value");
												record.Relationships_Kingdom3_isAtWar = relationshipsReader.GetAttribute("isAtWar");
												break;
										}
										c1++;
									} else if (relationshipsReader.GetAttribute("clan") != null) {
										switch (c2) {
											case 0:
												record.Relationships_Clan0_name = TrimD(relationshipsReader.GetAttribute("clan"));
												record.Relationships_Clan0_value = relationshipsReader.GetAttribute("value");
												record.Relationships_Clan0_isAtWar = relationshipsReader.GetAttribute("isAtWar");
												break;
											case 1:
												record.Relationships_Clan1_name = TrimD(relationshipsReader.GetAttribute("clan"));
												record.Relationships_Clan1_value = relationshipsReader.GetAttribute("value");
												record.Relationships_Clan1_isAtWar = relationshipsReader.GetAttribute("isAtWar");
												break;
											case 2:
												record.Relationships_Clan2_name = TrimD(relationshipsReader.GetAttribute("clan"));
												record.Relationships_Clan2_value = relationshipsReader.GetAttribute("value");
												record.Relationships_Clan2_isAtWar = relationshipsReader.GetAttribute("isAtWar");
												break;
											case 3:
												record.Relationships_Clan3_name = TrimD(relationshipsReader.GetAttribute("clan"));
												record.Relationships_Clan3_value = relationshipsReader.GetAttribute("value");
												record.Relationships_Clan3_isAtWar = relationshipsReader.GetAttribute("isAtWar");
												break;
										}
										c2++;

									} else Console.WriteLine("The xml document at '{0}' is invalid!", xmlInput);
								}
								break;
							case "policies":
								XmlReader policiesReader = sub.ReadSubtree();
								while(policiesReader.Read()) {
									if (!policiesReader.NodeType.Equals(XmlNodeType.Element) || policiesReader.Name.Equals("policies")) continue;
									switch(c3) {
										case 0:
											record.Policy0 = policiesReader.GetAttribute("id");
											break;
										case 1:
											record.Policy1 = policiesReader.GetAttribute("id");
											break;
										case 2:
											record.Policy2 = policiesReader.GetAttribute("id");
											break;
										case 3:
											record.Policy3 = policiesReader.GetAttribute("id");
											break;
									}
									c3++;
                                }
								break;
						}
						sub.Read();
					}

					records.Add(record);
					root.Read();
				}
				using (CsvWriter csvWriter = new CsvWriter(new StreamWriter(csvOutput), CultureInfo.InvariantCulture)) {
					csvWriter.WriteRecords(records);
					csvWriter.Flush();
				}
			}
		}
		public class KingdomRecord {
			public string id { get; set; }
			public string banner_key { get; set; }
			public string owner { get; set; }
			public string label_color { get; set; }
			public string primary_banner_color { get; set; }
			public string secondary_banner_color { get; set; }
			public string color { get; set; }
			public string color2 { get; set; }
			public string alternative_color { get; set; }
			public string alternative_color2 { get; set; }
			public string culture { get; set; }
			public string settlement_banner_mesh { get; set; }
			public string flag_mesh { get; set; }
			public string name { get; set; }
			public string short_name { get; set; }
			public string text { get; set; }
			public string title { get; set; }
			public string ruler_title { get; set; }

			public string Policy0 { get; set; }
			public string Policy1 { get; set; }
			public string Policy2 { get; set; }
			public string Policy3 { get; set; }

			public string Relationships_Kingdom0_name { get; set; }
			public string Relationships_Kingdom0_value { get; set; }
			public string Relationships_Kingdom0_isAtWar { get; set; }
			public string Relationships_Kingdom1_name { get; set; }
			public string Relationships_Kingdom1_value { get; set; }
			public string Relationships_Kingdom1_isAtWar { get; set; }
			public string Relationships_Kingdom2_name { get; set; }
			public string Relationships_Kingdom2_value { get; set; }
			public string Relationships_Kingdom2_isAtWar { get; set; }
			public string Relationships_Kingdom3_name { get; set; }
			public string Relationships_Kingdom3_value { get; set; }
			public string Relationships_Kingdom3_isAtWar { get; set; }

			public string Relationships_Clan0_name { get; set; }
			public string Relationships_Clan0_value { get; set; }
			public string Relationships_Clan0_isAtWar { get; set; }
			public string Relationships_Clan1_name { get; set; }
			public string Relationships_Clan1_value { get; set; }
			public string Relationships_Clan1_isAtWar { get; set; }
			public string Relationships_Clan2_name { get; set; }
			public string Relationships_Clan2_value { get; set; }
			public string Relationships_Clan2_isAtWar { get; set; }
			public string Relationships_Clan3_name { get; set; }
			public string Relationships_Clan3_value { get; set; }
			public string Relationships_Clan3_isAtWar { get; set; }
		}
	}
}
