set identity_insert HandicapMatrix on
merge into HandicapMatrix as Target
using (
	values
	( 1, 2, 2, 2, 2),
	( 2, 2, 3, 2, 3),
	( 3, 2, 4, 2, 4),
	( 4, 2, 5, 2, 5),
	( 5, 2, 6, 2, 6),
	( 6, 2, 7, 2, 7),
	( 7, 3, 2, 3, 2),
	( 8, 3, 3, 2, 2),
	( 9, 3, 4, 2, 3),
	( 10, 3, 5, 2, 4),
	( 11, 3, 6, 2, 5),
	( 12, 3, 7, 2, 6),
	( 13, 4, 2, 4, 2),
	( 14, 4, 3, 3, 2),
	( 15, 4, 4, 3, 3),
	( 16, 4, 5, 3, 4),
	( 17, 4, 6, 3, 5),
	( 18, 4, 7, 2, 5),
	( 19, 5, 2, 5, 2),
	( 20, 5, 3, 4, 2),
	( 21, 5, 4, 4, 3),
	( 22, 5, 5, 4, 4),
	( 23, 5, 6, 4, 5),
	( 24, 5, 7, 4, 6),
	( 25, 6, 2, 6, 2),
	( 26, 6, 3, 5, 2),
	( 27, 6, 4, 5, 3),
	( 28, 6, 5, 5, 4),
	( 29, 6, 6, 5, 5),
	( 30, 6, 7, 4, 5),
	( 31, 7, 2, 7, 2),
	( 32, 7, 3, 6, 2),
	( 33, 7, 4, 5, 2),
	( 34, 7, 5, 5, 3),
	( 35, 7, 6, 5, 4),
	( 36, 7, 7, 5, 5)
) as Source (HandicapMatrixId, Player1, Player2, Player1Wins, Player2Wins)
on target.HandicapMatrixId = Source.HandicapMatrixId
when matched then
update set 
	Player1 = Source.Player1,
	Player2 = Source.Player2,
	Player1Wins = Source.Player1Wins,
	Player2Wins = Source.Player2Wins
when not matched by target then
insert (HandicapMatrixId, Player1, Player2, Player1Wins, Player2Wins)
values (Source.HandicapMatrixId, Source.Player1, Source.Player2, Source.Player1Wins, Source.Player2Wins)
;
set identity_insert HandicapMatrix off