#include "Cell.h"
#include <iostream>

using namespace std;

Cell::Cell(char const letter) : _letter(letter), _EOW(false) {

} 

Cell::~Cell() {
	try {
		for (Cell* const child : _cellList) {
			delete child;
		}
	}
	catch(exception& e) { 

	}
}

Cell::Cell(const Cell &rhs) : _cellList(rhs._cellList), _letter(rhs._letter), _EOW(rhs._EOW) {

}

Cell& Cell::operator=(const Cell& rhs) {
	if (&rhs != this) {
		_cellList = rhs._cellList;
		_letter = rhs._letter;
		_EOW = rhs._EOW;
		return *this;
	}
}

Cell* Cell::CreateLetterOrGetLetter(char letter) {
	if (_cellList.size() > 0) {
		for (Cell* const checkCell : _cellList) {
			if (checkCell->GetLetter() == letter) {
				return checkCell;
			}				
		}
	}
	Cell* const newCell = new Cell(letter);
	_cellList.push_back(newCell);
	return newCell;
}
