using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace InitialProject.Service
{
    public class KeyPointService
    {
        KeyPointRepository keyPointRepository = new KeyPointRepository();

        public void CreateKeyPoint(KeyPointDTO keyPointDTO)
        {
            KeyPoint keyPoint = new KeyPoint();
            keyPoint.Name = keyPointDTO.Name;

            keyPointRepository.Save(keyPoint);
        }

        public void ChangeKeyPointStatus(KeyPoint keyPoint)
        {
            KeyPointRepository keyPointRepository = new KeyPointRepository();
            keyPoint.Status = Status.Ongoing;
            keyPointRepository.Update(keyPoint);
        }

        public List<int> IsTouristPresent(List<int> touristsToArrive)
        {
            List<int> arrivedTourists = new List<int>();

            Console.WriteLine("\nTourists to arrive: ");
            touristsToArrive.ForEach(Console.WriteLine);

            foreach (int tourist in touristsToArrive)
            {
                Console.WriteLine(tourist + "Arrived: (y/n)");
                string arrived = Console.ReadLine();

                if (arrived == "y")
                {
                    arrivedTourists.Add(tourist);
                }
            }
            return arrivedTourists;
        }

        public List<int> InitiateKeyPoint(KeyPoint keyPoint, List<KeyPoint> keyPoints, List<int> touristsToArrive)
        {
            ChangeKeyPointStatus(keyPoint);

            List<int> arrivedTourists = IsTouristPresent(touristsToArrive);

            keyPoint.ArrivedIds = arrivedTourists;

            FinishKeyPoint(keyPoint, keyPoints, touristsToArrive);

            return arrivedTourists;
        }

        public static void FinishKeyPoint(KeyPoint keyPoint, List<KeyPoint> keyPoints, List<int> touristsToArrive)
        {
            KeyPointRepository keyPointRepository = new KeyPointRepository();
            Console.WriteLine("\nStarted keyPoint " + keyPoint.Name + ". Do you want to finish key point? (y) ");
            string response = Console.ReadLine();
            switch (response)
            {
                case "y":
                    keyPoint.Status = Status.Finished;
                    keyPointRepository.Update(keyPoint);
                    break;
                default:
                    Console.WriteLine("Option does not exist");
                    break;
            }
        }

        public void FindAvailableKeyPoints(List<int> keyPointIds)
        {
            Console.WriteLine("\nAvailable key points:");
            foreach (int id in keyPointIds)
            {
                KeyPoint keyPoint = keyPointRepository.FindById(id);
                if (keyPoint.Status != Status.Finished)
                    Console.WriteLine(keyPoint.Id + " " + keyPoint.Name);
            }
        }

        public void SelectKeyPoint(List<KeyPoint> keyPoints, List<int> keyPointIds, List<int> touristsToArrive)
        {
            KeyPointRepository keyPointRepository = new KeyPointRepository();
            keyPointIds.Remove(keyPointIds.First());

            do
            {
                FindAvailableKeyPoints(keyPointIds);

                Console.WriteLine("\nSelect key point: ");
                int keyPointId = Convert.ToInt32(Console.ReadLine());
                keyPointIds.Remove(keyPointId);

                KeyPoint selectedKeyPoint = keyPointRepository.FindById(keyPointId);
                InitiateKeyPoint(selectedKeyPoint, keyPoints, touristsToArrive);
            } while (keyPointIds.Count > 0);
        }

    }
}

