INSERT INTO [dbo].[ApplicationUser]
(
	[Id],
    [UserName],
    [NormalizedUserName],
    [Email],
    [NormalizedEmail],
    [EmailConfirmed],
    [PasswordHash],
    [PhoneNumber],
    [PhoneNumberConfirmed],
    [TwoFactorEnabled]
)
VALUES 
(
	'ec3a0c5e-b4f2-4b3e-a126-ccc8a9daa196',
    'christobal_c@hotmail.com',
    'CHRISTOBAL_C@HOTMAIL.COM',
    'christobal_c@hotmail.com',
    'CHRISTOBAL_C@HOTMAIL.COM',
    0,
    'AQAAAAEAACcQAAAAEAVsqMt73pzPPXUvcYNGL+TtZI6J9GK3oGsh5Y8sXzEDuWWOAP777okQDxVDZG7V0w==',
    '070',
    0,
    0
)

INSERT INTO ApplicationRole (Id,Name, NormalizedName) VALUES ('df619159-2e67-4a38-89d4-d80cedfbf341','admin', 'ADMIN')
INSERT INTO ApplicationUserRole (UserId, RoleId) VALUES ('ec3a0c5e-b4f2-4b3e-a126-ccc8a9daa196','df619159-2e67-4a38-89d4-d80cedfbf341')