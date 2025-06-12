/*==============================================================*/
/* Nom de SGBD :  PostgreSQL 8                                  */
/* Date de création :  05/06/2025 11:18:07                      */
/*==============================================================*/


drop table  IF EXISTS  APPELATION CASCADE;

drop table  IF EXISTS  CLIENT CASCADE;

drop table  IF EXISTS  COMMANDE CASCADE;

drop table  IF EXISTS  DEMANDE CASCADE;

drop table  IF EXISTS  DETAILCOMMANDE CASCADE;

drop table  IF EXISTS  EMPLOYE CASCADE;

drop table  IF EXISTS  FOURNISSEUR CASCADE;

drop table  IF EXISTS  ROLE CASCADE;

drop table  IF EXISTS  TYPEVIN CASCADE;

drop table  IF EXISTS  VIN CASCADE;

/*==============================================================*/
/* Table : APPELATION                                           */
/*==============================================================*/
create table APPELATION (
   NUMAPPELATION             SERIAL               not null,
   NOMAPPELATION        VARCHAR(30)          null,
   constraint PK_APPELATION primary key (NUMAPPELATION)
);

/*==============================================================*/
/* Table : CLIENT                                               */
/*==============================================================*/
create table CLIENT (
   NUMCLIENT            SERIAL               not null,
   NOMCLIENT            VARCHAR(50)          null,
   PRENOMCLIENT         VARCHAR(50)          null,
   MAILCLIENT           VARCHAR(100)         null,
   constraint PK_CLIENT primary key (NUMCLIENT)
);

/*==============================================================*/
/* Table : COMMANDE                                             */
/*==============================================================*/
create table COMMANDE (
   NUMCOMMANDE          SERIAL               not null,
   NUMEMPLOYE           INT4                 not null,
   DATECOMMANDE         DATE                 null,
   VALIDER              BOOL                 null,
   PRIXTOTAL            DECIMAL(6,2)         null,
   constraint PK_COMMANDE primary key (NUMCOMMANDE)
);

/*==============================================================*/
/* Table : DEMANDE                                              */
/*==============================================================*/
create table DEMANDE (
   NUMDEMANDE           SERIAL               not null,
   NUMVIN               INT4                 not null,
   NUMEMPLOYE           INT4                 not null,
   NUMCOMMANDE          INT4                 not null,
   NUMCLIENT            INT4                 not null,
   DATEDEMANDE          DATE                 null,
   QUANTITEDEMANDE      INT4                 null,
   ACCEPTER             VARCHAR(30)          null,
   constraint PK_DEMANDE primary key (NUMDEMANDE)
);

/*==============================================================*/
/* Table : DETAILCOMMANDE                                       */
/*==============================================================*/
create table DETAILCOMMANDE (
   NUMCOMMANDE          INT4                 not null,
   NUMVIN               INT4                 not null,
   QUANTITE             INT4                 null
      constraint CKC_QUANTITE_DETAILCO check (QUANTITE is null or (QUANTITE between 1 and 100)),
   PRIX                 DECIMAL(6,2)         null,
   constraint PK_DETAILCOMMANDE primary key (NUMCOMMANDE, NUMVIN)
);

/*==============================================================*/
/* Table : EMPLOYE                                              */
/*==============================================================*/
create table EMPLOYE (
   NUMEMPLOYE           SERIAL               not null,
   NUMROLE              INT4                 not null,
   NOM                  VARCHAR(30)          null,
   PRENOM               VARCHAR(30)          null,
   LOGIN                VARCHAR(30)          null,
   MDP                  VARCHAR(30)          null,
   constraint PK_EMPLOYE primary key (NUMEMPLOYE)
);

/*==============================================================*/
/* Table : FOURNISSEUR                                          */
/*==============================================================*/
create table FOURNISSEUR (
   NUMFOURNISSEUR       SERIAL               not null,
   NOMFOURNISSEUR       VARCHAR(30)          null,
   constraint PK_FOURNISSEUR primary key (NUMFOURNISSEUR)
);

/*==============================================================*/
/* Table : ROLE                                                 */
/*==============================================================*/
create table ROLE (
   NUMROLE              SERIAL               not null,
   NOMROLE              VARCHAR(30)          null,
   constraint PK_ROLE primary key (NUMROLE)
);

/*==============================================================*/
/* Table : TYPEVIN                                              */
/*==============================================================*/
create table TYPEVIN (
   NUMTYPE              SERIAL               not null,
   NOMTYPE              VARCHAR(30)          null,
   constraint PK_TYPEVIN primary key (NUMTYPE)
);

/*==============================================================*/
/* Table : VIN                                                  */
/*==============================================================*/
create table VIN (
   NUMVIN               SERIAL               not null,
   NUMFOURNISSEUR       INT4                 not null,
   NUMTYPE              INT4                 not null,
   NUMAPPELATION            INT4                 not null,
   NOMVIN               VARCHAR(100)         null,
   PRIXVIN              DECIMAL(6,2)         null,
   DESCRIPTIF           VARCHAR(300)         null,
   MILLESIME            INT4                 null,
   constraint PK_VIN primary key (NUMVIN)
);

alter table COMMANDE
   add constraint FK_COMMANDE_REALISE_EMPLOYE foreign key (NUMEMPLOYE)
      references EMPLOYE (NUMEMPLOYE)
      on delete restrict on update restrict;

alter table DEMANDE
   add constraint FK_DEMANDE_CONCERNE_VIN foreign key (NUMVIN)
      references VIN (NUMVIN)
      on delete restrict on update restrict;

alter table DEMANDE
   add constraint FK_DEMANDE_ENREGISTR_EMPLOYE foreign key (NUMEMPLOYE)
      references EMPLOYE (NUMEMPLOYE)
      on delete restrict on update restrict;

alter table DEMANDE
   add constraint FK_DEMANDE_FAIRE_CLIENT foreign key (NUMCLIENT)
      references CLIENT (NUMCLIENT)
      on delete restrict on update restrict;

alter table DEMANDE
   add constraint FK_DEMANDE_LIENDDECD_COMMANDE foreign key (NUMCOMMANDE)
      references COMMANDE (NUMCOMMANDE)
      on delete restrict on update restrict;

alter table DETAILCOMMANDE
   add constraint FK_DETAILCO_DETAILCOM_COMMANDE foreign key (NUMCOMMANDE)
      references COMMANDE (NUMCOMMANDE)
      on delete restrict on update restrict;

alter table DETAILCOMMANDE
   add constraint FK_DETAILCO_DETAILCOM_VIN foreign key (NUMVIN)
      references VIN (NUMVIN)
      on delete restrict on update restrict;

alter table EMPLOYE
   add constraint FK_EMPLOYE_LIE_ROLE foreign key (NUMROLE)
      references ROLE (NUMROLE)
      on delete restrict on update restrict;

alter table VIN
   add constraint FK_VIN_APPARTIEN_APPELATI foreign key (NUMAPPELATION)
      references APPELATION (NUMAPPELATION)
      on delete restrict on update restrict;

alter table VIN
   add constraint FK_VIN_CARACTERI_TYPEVIN foreign key (NUMTYPE)
      references TYPEVIN (NUMTYPE)
      on delete restrict on update restrict;

alter table VIN
   add constraint FK_VIN_FOURNIT_FOURNISS foreign key (NUMFOURNISSEUR)
      references FOURNISSEUR (NUMFOURNISSEUR)
      on delete restrict on update restrict;

INSERT INTO ROLE (NOMROLE) VALUES
('Responsable Magasin'),
('Vendeur');
INSERT INTO EMPLOYE (NUMROLE, NOM, PRENOM, LOGIN, MDP) VALUES
(2, 'Lucas', 'Georges', 'celine98', 'mdp0'),
(1, 'Masse', 'Christine', 'eliseevrard', 'mdp1'),
(1, 'Rolland', 'Josette', 'juliengeorges', 'mdp2'),
(2, 'Delahaye', 'Patricia', 'chauveaupatrick', 'mdp3'),
(2, 'Thierry', 'Zacharie', 'dorothee64', 'mdp4'),
(1, 'Leclercq', 'Sophie', 'jolychristophe', 'mdp5'),
(1, 'Imbert', 'Benoît', 'seguinsabine', 'mdp6'),
(1, 'Dupont', 'Clémence', 'mendesagathe', 'mdp7'),
(2, 'Gomez', 'Tristan', 'nloiseau', 'mdp8'),
(1, 'Bigot', 'Valérie', 'virginie33', 'mdp9');
INSERT INTO CLIENT (NOMCLIENT, PRENOMCLIENT, MAILCLIENT) VALUES
('Fischer', 'Cécile', 'benjamin72@dumont.fr'),
('Guibert', 'Olivier', 'mmartineau@wanadoo.fr'),
('Sanchez', 'Antoinette', 'martyjean@jacob.com'),
('Grondin', 'Brigitte', 'hjean@tele2.fr'),
('Merle', 'Marianne', 'xavier71@sfr.fr'),
('Meunier', 'Frédéric', 'vollivier@vallee.net'),
('Coste', 'Bertrand', 'antoinetteletellier@begue.com'),
('Wagner', 'Éléonore', 'renardbenoit@dufour.com'),
('Girard', 'Olivie', 'valeriedelorme@leroy.fr'),
('Joly', 'Antoine', 'bazinnicole@voila.fr');
INSERT INTO FOURNISSEUR (NOMFOURNISSEUR) VALUES
('Vins du Sud'),
('Château Bordeaux'),
('Domaine de la Loire'),
('Les Vignes Bleues'),
('Terroir & Tradition');
INSERT INTO APPELATION (NOMAPPELATION) VALUES
('Bordeaux'),
('Côtes du Rhône'),
('Sancerre'),
('Champagne'),
('Alsace');
INSERT INTO TYPEVIN (NOMTYPE) VALUES
('Rouge'),
('Blanc'),
('Rosé');
INSERT INTO VIN (NUMFOURNISSEUR, NUMTYPE, NUMAPPELATION, NOMVIN, PRIXVIN, DESCRIPTIF, MILLESIME) VALUES
(5, 1, 5, 'Somme Champagne', 12.86, 'Aucun protéger service et affirmer accord attendre branche exister.', 2016),
(2, 1, 5, 'Lui Alsace', 12.39, 'Situation installer rejoindre couper marquer continuer grain vaincre lui autour ah.', 2018),
(5, 2, 2, 'Drame Champagne', 63.03, 'Me compte contenter souvenir finir refuser trembler reprendre jeu.', 2015),
(2, 3, 4, 'Fille Sancerre', 35.01, 'Saisir bruit choisir regretter ouvert siège noire.', 2018),
(3, 1, 1, 'Groupe Champagne', 18.7, 'Renverser réserver en étudier admettre éloigner lieu coup feuille gens.', 2020),
(5, 2, 1, 'Justice Champagne', 58.26, 'Place parti ignorer enfant pencher si examiner atteindre semblable distinguer mouvement voici nerveux.', 2021),
(1, 3, 3, 'Carte Alsace', 89.69, 'Lutte installer pauvre croiser peuple tenir rejoindre groupe dehors éteindre rue souffrance face.', 2020),
(5, 1, 1, 'Yeux Bordeaux', 69.51, 'Importance exemple se secrétaire face beau mêler mieux nation exprimer accompagner champ.', 2019),
(1, 1, 1, 'Verre Champagne', 35.02, 'Penser menacer haut sourire vin semaine adresser accorder consentir voisin parler souhaiter matière.', 2020),
(2, 2, 3, 'Réussir Côtes du Rhône', 70.32, 'Blond salle connaissance général tomber acheter.', 2016);
INSERT INTO COMMANDE (NUMEMPLOYE, DATECOMMANDE, VALIDER, PRIXTOTAL) VALUES
(10, '2024-07-03', True, 169.56),
(4, '2024-08-24', True, 149.43),
(5, '2024-06-16', True, 211.69),
(1, '2024-12-29', True, 250.1),
(6, '2025-04-05', False, 94.97),
(4, '2024-06-17', False, 79.54),
(8, '2024-12-20', False, 267.71),
(8, '2025-04-24', True, 94.17),
(4, '2024-12-05', False, 229.16),
(7, '2024-09-03', False, 121.36);
INSERT INTO DETAILCOMMANDE (NUMCOMMANDE, NUMVIN, QUANTITE, PRIX) VALUES
(9, 8, 2, 78.02),
(2, 3, 3, 81.29),
(7, 10, 2, 44.63),
(10, 8, 9, 32.63),
(9, 2, 2, 71.35),
(9, 5, 6, 20.04),
(7, 3, 8, 10.29),
(5, 9, 3, 55.69),
(2, 5, 9, 64.81),
(3, 6, 3, 58.54),
(9, 1, 10, 39.17),
(1, 2, 6, 89.08),
(5, 4, 1, 31.68),
(10, 2, 2, 75.87),
(8, 4, 4, 140.04),
(4, 6, 2, 116.52),
(6, 7, 8, 717.52),
(2, 9, 3, 21.56);
INSERT INTO DEMANDE (NUMVIN, NUMEMPLOYE, NUMCOMMANDE, NUMCLIENT, DATEDEMANDE, QUANTITEDEMANDE, ACCEPTER) VALUES
(8, 9, 3, 5, '2024-11-27', 17, 'En attente'),
(7, 4, 9, 4, '2025-05-27', 10, 'Non'),
(6, 8, 9, 8, '2025-03-26', 4, 'Oui'),
(4, 2, 6, 1, '2024-08-24', 19, 'En attente'),
(4, 10, 4, 1, '2025-04-13', 3, 'En attente'),
(1, 4, 2, 1, '2024-07-04', 11, 'Oui'),
(9, 4, 5, 8, '2024-10-06', 7, 'En attente'),
(3, 10, 10, 8, '2024-06-27', 8, 'Non'),
(7, 4, 2, 2, '2025-01-18', 14, 'Non'),
(7, 7, 8, 1, '2025-02-03', 4, 'Oui');
