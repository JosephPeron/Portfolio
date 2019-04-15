#pragma once
#include <list>

class Cell {
	std::list<Cell*> _cellList;
	char _letter;
	bool _EOW;

public:
	explicit Cell(char letter);
	~Cell();	
	Cell(const Cell &rhs);
	Cell& Cell::operator=(const Cell& b);
	char GetLetter() const { return _letter; };
	bool GetEOW() const { return _EOW; };
	const std::list<Cell*>& GetCellList() const { return _cellList; };
	void SetEndBool() { _EOW = true; };
	Cell* CreateLetterOrGetLetter(char letter);


};