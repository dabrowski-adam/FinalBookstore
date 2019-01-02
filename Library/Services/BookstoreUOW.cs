﻿using BookstoreLogic.Data;
using BookstoreLogic.Logic;
using BookstoreLogic.LogicImplementations;

namespace BookstoreLogic.Services
{
    public class BookstoreUOW
    {
        private BookstoreState BookstoreData;

        private IBookDao booksDao;
        private IRentalDao invoicesDao;

        public BookstoreUOW(BookstoreState bookstoreData, IBookDao bDao, IRentalDao rDao)
        {
            BookstoreData = bookstoreData;

            booksDao = bDao;
            invoicesDao = rDao;
        }

        public void FillBookstoreState(IBookstoreFiller filler)
        {
            filler.Fill(BookstoreData);
        }


        
        public void RemoveAllData()
        {
            RemoveAllInvoices();
            RemoveAllBooks();
        }

        public void RemoveAllBooks()
        {
            for (int i = BookstoreData.BookstoreBooks.Count - 1; i >= 0; i--)
            {
                int bookISBN = BookstoreData.BookstoreBooks[i].ISBN;

                if (booksDao.CanRemoveBook(bookISBN))
                    booksDao.RemoveBook(bookISBN);
            }
        }

  
        public void RemoveAllInvoices()
        {
            BookstoreData.BookInvoices.Clear();
        }


        // Getters (return left side if left side != null, otherwiser return right side)
        public IBookDao GetBooksDao => booksDao ?? (booksDao = new BookDaoBasicImpl(BookstoreData));
        public IRentalDao GetInvoicesDao => invoicesDao ?? (invoicesDao = new RentalDaoBasicImpl(BookstoreData));
    }
}