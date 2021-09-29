/*
Script de déploiement pour DbGestContact

Ce code a été généré par un outil.
La modification de ce fichier peut provoquer un comportement incorrect et sera perdue si
le code est régénéré.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "DbGestContact"
:setvar DefaultFilePrefix "DbGestContact"
:setvar DefaultDataPath "C:\Users\comat\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\"
:setvar DefaultLogPath "C:\Users\comat\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\"

GO
:on error exit
GO
/*
Détectez le mode SQLCMD et désactivez l'exécution du script si le mode SQLCMD n'est pas pris en charge.
Pour réactiver le script une fois le mode SQLCMD activé, exécutez ce qui suit :
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'Le mode SQLCMD doit être activé de manière à pouvoir exécuter ce script.';
        SET NOEXEC ON;
    END


GO
USE [master];


GO

IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'Création de la base de données $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)]
    ON 
    PRIMARY(NAME = [$(DatabaseName)], FILENAME = N'$(DefaultDataPath)$(DefaultFilePrefix)_Primary.mdf')
    LOG ON (NAME = [$(DatabaseName)_log], FILENAME = N'$(DefaultLogPath)$(DefaultFilePrefix)_Primary.ldf') COLLATE SQL_Latin1_General_CP1_CI_AS
GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
USE [$(DatabaseName)];


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'Impossible de modifier les paramètres de base de données. Vous devez être administrateur système pour appliquer ces paramètres.';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'Impossible de modifier les paramètres de base de données. Vous devez être administrateur système pour appliquer ces paramètres.';
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET FILESTREAM(NON_TRANSACTED_ACCESS = OFF),
                CONTAINMENT = NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CREATE_STATISTICS ON(INCREMENTAL = OFF),
                MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT = OFF,
                DELAYED_DURABILITY = DISABLED 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE (QUERY_CAPTURE_MODE = ALL, DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_PLANS_PER_QUERY = 200, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 367), MAX_STORAGE_SIZE_MB = 100) 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE = OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
    END


GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


GO
PRINT N'Création de Table [dbo].[Contact]...';


GO
CREATE TABLE [dbo].[Contact] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [LastName]  NVARCHAR (75)  NOT NULL,
    [FirstName] NVARCHAR (75)  NOT NULL,
    [Email]     NVARCHAR (384) NOT NULL,
    [BirthDate] DATETIME2 (7)  NOT NULL,
    [UserId]    INT            NOT NULL,
    CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Création de Table [dbo].[User]...';


GO
CREATE TABLE [dbo].[User] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [LastName]  NVARCHAR (75)  NOT NULL,
    [FirstName] NVARCHAR (75)  NOT NULL,
    [Email]     NVARCHAR (384) NOT NULL,
    [Passwd]    BINARY (64)    NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UK_User_Email] UNIQUE NONCLUSTERED ([Email] ASC)
);


GO
PRINT N'Création de Clé étrangère [dbo].[FK_Contact_User]...';


GO
ALTER TABLE [dbo].[Contact]
    ADD CONSTRAINT [FK_Contact_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]);


GO
PRINT N'Création de Fonction [dbo].[GetPostSalt]...';


GO
CREATE FUNCTION [dbo].[GetPostSalt]()
RETURNS NCHAR(2048)
AS
BEGIN
	RETURN N'PBr!&rd+!%H5g=w#^z745aTbAqcb7^2Sr_v_NQD9DGcVn$DZG&sR82h^Hqj_3$NCny^AWD+T_*Zvf5-H7KhV%xzaKa%xZyy$+RwXmt%64sjhpAnf+9_NZu!^CE7d4Ztau_Yzqd6mLLRXJe*&n+vMZP-cb!Rh3NX*9Yw5+DUPdKkm$kMJC$3m8ScV8p@37sER-K3CRL3PZV&wVhDcsadfj%cMYTG#sT?_G5%T2EJYUg@amj$KBgAV9?pQ%QWUEK4pb4EcvrPeD?-QHsNbU4=cW+8hEHHUtwsL$sHMtW=VCFm^n$waCSFTqA?Tcc*UTsd++VMnb%vr+g_sSaf9Z_uJ8a9AjA#aJsHa?cLU!Y4LgkPT*KAMAHPqxBaXL!BFaDPC+Zx+eKwHpzK9LJ4fKJU=^P!kjdg7sqhQsjd@QrN5N=xPpsc4hzAu8EQd*rg6G8%-Ey!&P=^+SJ7xqG@mcwcTARX@s*=qSH%dTE@U*P&KBRps+F=UP3HjrcYzJPNuGm@w2H6QU+?Sb+m+gdaHRa#ZKk38payV-!&nd5yPMXjuQ4eazf%LMrAB_LL2UK3k-mz7TwUr_?!!q!mJCq9MBru9aJen^VffykmsHMwSh!%n=V8gr$5@FjSHAkb6HDZZJCa+evWNdTYFt_YSuJ6kJMhb!bG@8hz_Y!K@k-zajKnqb%*-Bq9rc?8F!5=A$SkxMBSv_-z*QW3YAvPdFe2GD78t!&r4AGp8k5ZukVunarLw_qMBHzs7q_p+8k!PqA298z?cmwJ&h-QcZBLefnUJps9pC_pMdaGMKmqz4B-ub-Qn&9+5j-^%#3a67MBRQqdEG?NCBkx?$sP?%AHH?4cWFSe5uQ67E$=2L@4W2r43WKbP=ftyJbevjjgc=MfmFs&TSa&bdS$2BBnV#=DYrT5zAMLuYgbb6p$H4gFjQQKh+9Lk5?*Mxu$YUxB&yurKD7Bea*Fv+ESrj$MUZsu8n9JWcLb#-Q!U-d%-XQSfFX4^uG_gkGag6dQ?^N4n^%$rqxnjucxpf#EWAs#+gkG=ztY_N%55X$ZvJnH*+m*J4?EPatDJBwM+@Q89N9d_^_ZmCXPb$CRhX+m5eburYT_$A%?W^968A4$2bBvfUE%DH7UGkK76U4Mb_a#&q_EfHnYQy-XLGjKT-&Q#8smb*w8r4V_?jE9cXqjb9ctkf!4s+XM&Kn^?NLP^SvyF!kxkNfq#MBkh3J#vB9anY6y2e?W6Rdm66+TLVbFAkBxWmesz&Q=AyH#=sYSj9$w?PN4mZ!LE=Ram=KCE5+x#Q%tAK5=mcqDGkvp-dx3VanU++J7BMuR@Cm+nEx6fVgefWaZKmB$&$w74DJmkpNpnpAwdVfs$*+#=cNtfpjUYcJU%K3#ULgShTsN#XRqwZK8JRTv!9HUAPwV@X*jT^JfYF$cPwXvGL+fdbz&wm3NXsVxa#ceeKstJwMcxg9Rw*Gc9b^B99Cf82ApRNUMdxMnzB75Mz2-=qs3c=BcQ6uf!5RqwguLctc9jL5xUr%EuVYeL@Qy6vGQ_7KYSXvd=H^bCtW=Urs4ZY*2_#tcMTZ_8&Kkwj&QzDkh+gA8YJGTD_&*hsK^ScND&sYdBtmM57WHQ=tdrJTG!?Ye+xYVN^EqbYxPLPM9Zk_-rPAsmyAWyW2#5XMx4d5uBYEeczs$D^7UgtdNPuLvfg*wQZhfreh52V!*RqtcnX=Hze@t64t^XFvCF_kcy7N#BY&&ZyA+*%B_BP6R+hsc#89zr3$=_q*MCav7uBxRZ*mQyP*G_%y8nTN^WYkWWY?jWP=#*sAHYZb+jFab5ZGNWhC6gCc%gu*K6R=$emD+LZs6EVDwR89TCTsfdSs*FxLQHKSU*GS!V3=w8jAyjzfqFg*d!GpEm*jBb6VPJuV=D8jMuFp*B=Vpc@=!sD*PVWVpddZ5tuZx^k!X-&UgA!mU3mU=KJUR!db^E#*WzpYXQ$Qq4En@uu4QW?^u?c^XdB4k55UfjnyVj^7*A*bmywYA@&FftGy&!5RKFAwFFs3*ADm+m@@MNRHpHe+yb64*LBTHDg8=L_G7x';
END
GO
PRINT N'Création de Fonction [dbo].[GetPreSalt]...';


GO
CREATE FUNCTION [dbo].[GetPreSalt]()
RETURNS NCHAR(2048)
AS
BEGIN
	RETURN N'sjhnGaU2C@9+2c_mRzWk^kmpCugyffSUS=az_cbfPA@jk?R$X3Xqes=3U!FJndQ3qn_6zn$MCAbt^Sf3HkC?X2xZc+49x5e*8jArz7SegMApp6R=A$Wab!B7X+jzFs!DQZE#fPMf7D+@um2XT%5!e&Qm6PeEmdn3XN@+cN7+mkV*wzEm^!T=*6$WcKMP&ZpUJ*KwpkFS9gPX3tfnxM7LUD&Ea^2&#w4Hdjb2NnUPtssLnfF2trb2G%t@*Ws+&8yfLeG+pp!ExPC_3^%q8nz9T$bDHjBD^nabRV^$cYuVTatttwG6CDt@N_&m=ZqHyYPUbhGQ56dn2Z@vtNGU@nzk+dyh$-q4FqcXQTGMa&zzc=9&ew&mf2V&4c$2*S_dGLQU8-BZYe8unGb$vj3$Q@QvYFApj4rZH-CL-HFU8wj7rR!C6h8nTaBtY4p3v55fXwnyn5euETDXk=7Btj*Nw2Pw=jaBc-bFaxwm$KKZJ7kD7F5B&Vr!nN*EzAX+ss-C!sf&uCnQnFXuPtLaGR@za_7x?c%sL3e9&jfCRpZ6CHL&J@%&#%spM+485pQnM=8FjdnyA^MTURHC@uL&=vGv@exEF%P#MQe6Yuzun!A9EpBPh%86B?qtrrVM_6n4#Xu*uBfTQZ?mFg#qfCQZjr^7edLLZxx^^ET6C+?vc?@eptz&UU4+U3m%zxvgbNjW4$X8jgv=M%wf=mCj2U#eA?WdeX4Q_BtcW@r6$=$w+XU5kE&++a?WZ?x4tNQwdL$cB$T*b*2vtUgarY+a5uNAW_*tr-%HtB5XF68jS?kGwN*^&GDYzW9X_Mrb%L9Q=LZEPgCNQ@nf+jyRmp_?5^CF2_ZM2P%xr2pF3?9@*ehQF@YhsxtrJB^hY?haS^CfTQnx-+3d-B6Z2YV2*x#NCZBVHYfC6-BWy#F?FaakpCJjzh+fFJcU^X=JdbfdRjxAUy$a4py8nhHSEFYv@CHs@ASZqD6Z-t9R%Az?^wqHL=GJLyceynDnx$*%en=p33#*VjrF%PvHSA-x^X+BuBGKQ@KR9jy*&qvUZN+%@c%#k7^%$atBf^pTm=g+xEL@hU5jwrXg+Mugsrmc5qeKcV&qm@S$R@CApw?xJVRwPVH%Z@-B_q3$4nkrMrpz_XVj4GfLwGU2Vyjrcw9dKytj7%&2uC%8h7ANP!WucF%F7-m%6x&%WxmzLu+MuN8ymh?#N-c89$txVnpj_QYq%4*P&=ZgdyGYnBV7Nb-9szLxks2^?KE!YcTfTY9-Tu4#^e9*dKb9Ku-tPJd^M%xG?_NTTXdDcUHg?3SA4F%p$qzm^3mBp&J4=VZwa2a7f_C=kGxxj7syLMhtxFYVZdF=QAZDqzuJn84%#gn+yDQYg9ZUJFRgYJfSZrZhRntGB@ek$dgR^qY6kELK%qZr?FGjhksCCXzxWSbRgkzK2A*=!q@^Q#+G6QmH=&tA@!ED6vYXVg!Rur_npE4+eK?g&5n8MbD&Jw7+Knf4jYgZmB&G^H+$qUjvx=@PxRN^9t4&TuMSHCkjrAq9pkwP6@P5@Hqx#ycY_A^9nXRcf?vu8kuA!s?n#-$+WbAyMvyHzCqyG7bBSa6j&YBtfMen5G*MYpX82VgnGmU^J?fw?v^p3QC4Gz^D2TZdC#fA6$FEm7pJq%k!UVWZB%Qb=gKwK&zA2BAA+z?+w4AXj!*4t$b5chsptkg5=BxzRaqM-pn4Mb^hypm!9SCZvv^mxvv-k7JJG%sVzHE^hdSP3azn%cbQuQJ7-qjCDpw@m!9bbx8*L8-vGm59#ERMxNqH@^+AvGfbC#7qNcj%^7s&yFE_FbGA3sT=!u-D%Zr7SJR!zxhM$SdVWM=?W*&AfUQFv*7EUYfQ#F-zCz62$FN?!KxUV3&Df!%X^455+JRkG$M-K^KbRKz%ueQ%-y3XUsGFb=u-5MTfb!$Hmv$2C%s4bXj_K4s2n7SDFS9?-7y&wL-K#zV5wDHE!9av*!bhMp*rYv-bn2BtdKkbfUCuqeeg9g3K2f=_rzP66DD=SDL5a4$5f_WK&Ve%Wp=6qMd+3eGp9UJjRgm+V4#H';;
END
GO
PRINT N'Création de Procédure [dbo].[AddContact]...';


GO
CREATE PROCEDURE [dbo].[AddContact]
	@LastName NVARCHAR(75),
	@FirstName NVARCHAR(75),
	@Email NVARCHAR(75),
	@BirthDate DateTime2,
	@UserId int
AS
BEGIN
	INSERT INTO [Contact] ([LastName], [FirstName], [Email], [BirthDate], [UserId]) VALUES (@LastName, @FirstName, @Email, @BirthDate, @UserId);
	RETURN 0
END
GO
PRINT N'Création de Procédure [dbo].[DeleteContact]...';


GO
CREATE PROCEDURE [dbo].[DeleteContact]
	@Id int
AS
BEGIN
	Delete From Contact Where Id = @Id
	RETURN 0
END
GO
PRINT N'Création de Procédure [dbo].[LoginUser]...';


GO
CREATE PROCEDURE [dbo].[LoginUser]
	@Email NVARCHAR(384),
	@Passwd NVARCHAR(20)
AS
BEGIN
	SELECT [Id], LastName, FirstName, Email
	FROM [User] 
	WHERE Email = @Email
	AND Passwd = HASHBYTES('SHA2_512', dbo.GetPreSalt() + @Passwd + dbo.GetPostSalt());
	RETURN 0;
END
GO
PRINT N'Création de Procédure [dbo].[RegisterUser]...';


GO
CREATE PROCEDURE [dbo].[RegisterUser]
	@LastName NVARCHAR(75),
	@FirstName NVARCHAR(75),
	@Email NVARCHAR(384),
	@Passwd NVARCHAR(20)
AS
BEGIN
	INSERT INTO [User] (LastName, FirstName, Email, Passwd) VALUES 
	(@LastName, @FirstName, @Email, HASHBYTES('SHA2_512', dbo.GetPreSalt() + @Passwd + dbo.GetPostSalt()));

	RETURN 0;
END
GO
PRINT N'Création de Procédure [dbo].[UpdateContact]...';


GO
CREATE PROCEDURE [dbo].[UpdateContact]
	@Id int,
	@LastName NVARCHAR(75),
	@FirstName NVARCHAR(75),
	@Email NVARCHAR(75),
	@BithDate DateTime2,
	@UserId INT
AS
BEGIN
	UPDATE [Contact] Set [LastName] = @LastName, [FirstName] = @FirstName, [Email] = @Email, [BirthDate] = @BithDate WHERE Id = @Id and UserId = @UserId;
	RETURN 0
END
GO
/*
Modèle de script de post-déploiement							
--------------------------------------------------------------------------------------
 Ce fichier contient des instructions SQL qui seront ajoutées au script de compilation.		
 Utilisez la syntaxe SQLCMD pour inclure un fichier dans le script de post-déploiement.			
 Exemple :      :r .\monfichier.sql								
 Utilisez la syntaxe SQLCMD pour référencer une variable dans le script de post-déploiement.		
 Exemple :      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
EXEC RegisterUser @LastName ='Adnet',
                  @FirstName ='Geoffroy',
                  @Email = 'mail@mail.be',
                  @Passwd = 'Azerty123456!'

EXEC AddContact @LastName = 'Michel',
                  @FirstName = 'Aurélie',
                  @Email = 'mamour@mail.com',
                  @BirthDate = '1984-03-12',
                  @UserId = 1
GO

GO
DECLARE @VarDecimalSupported AS BIT;

SELECT @VarDecimalSupported = 0;

IF ((ServerProperty(N'EngineEdition') = 3)
    AND (((@@microsoftversion / power(2, 24) = 9)
          AND (@@microsoftversion & 0xffff >= 3024))
         OR ((@@microsoftversion / power(2, 24) = 10)
             AND (@@microsoftversion & 0xffff >= 1600))))
    SELECT @VarDecimalSupported = 1;

IF (@VarDecimalSupported > 0)
    BEGIN
        EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
    END


GO
PRINT N'Mise à jour terminée.';


GO
