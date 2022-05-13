﻿using HypBLL.Interfaces;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HypBLL
{
    public class HYP : IHYP
    {
        private Application wordApp;
        public HYP(Application wordApp)
        {
            this.wordApp = wordApp;
        }
        public Application HYPExecute()
        {
            if (wordApp == null)
                throw new Exception("No Document Given");

            wordApp = HYPconsonants(wordApp);
            wordApp = HYPwovels(wordApp);
            wordApp = HYPcleanfirst(wordApp);
            wordApp = HYPcleanlast(wordApp);
            wordApp = HYPcleanlastconpunct(wordApp);

            return wordApp;
        }
        public Application HYPconsonants(Application wordApp)
        {
            Console.WriteLine("st_cons");
            int[][] varData = new int[757][]
            {
                new int[2]{ 4305, 4306 },
                new int[2]{ 4305, 4307 },
                new int[2]{ 4305, 4309 },
                new int[2]{ 4305, 4310 },
                new int[2]{ 4305, 4311 },
                new int[2]{ 4305, 4313 },
                new int[2]{ 4305, 4314 },
                new int[2]{ 4305, 4315 },
                new int[2]{ 4305, 4316 },
                new int[2]{ 4305, 4318 },
                new int[2]{ 4305, 4319 },
                new int[2]{ 4305, 4320 },
                new int[2]{ 4305, 4321 },
                new int[2]{ 4305, 4322 },
                new int[2]{ 4305, 4324 },
                new int[2]{ 4305, 4325 },
                new int[2]{ 4305, 4326 },
                new int[2]{ 4305, 4327 },
                new int[2]{ 4305, 4328 },
                new int[2]{ 4305, 4329 },
                new int[2]{ 4305, 4330 },
                new int[2]{ 4305, 4331 },
                new int[2]{ 4305, 4332 },
                new int[2]{ 4305, 4333 },
                new int[2]{ 4305, 4334 },
                new int[2]{ 4305, 4335 },
                new int[2]{ 4305, 4336 },
                new int[2]{ 4306, 4305 },
                new int[2]{ 4306, 4307 },
                new int[2]{ 4306, 4309 },
                new int[2]{ 4306, 4310 },
                new int[2]{ 4306, 4311 },
                new int[2]{ 4306, 4313 },
                new int[2]{ 4306, 4314 },
                new int[2]{ 4306, 4315 },
                new int[2]{ 4306, 4316 },
                new int[2]{ 4306, 4318 },
                new int[2]{ 4306, 4319 },
                new int[2]{ 4306, 4320 },
                new int[2]{ 4306, 4321 },
                new int[2]{ 4306, 4322 },
                new int[2]{ 4306, 4324 },
                new int[2]{ 4306, 4325 },
                new int[2]{ 4306, 4326 },
                new int[2]{ 4306, 4327 },
                new int[2]{ 4306, 4328 },
                new int[2]{ 4306, 4329 },
                new int[2]{ 4306, 4330 },
                new int[2]{ 4306, 4331 },
                new int[2]{ 4306, 4332 },
                new int[2]{ 4306, 4333 },
                new int[2]{ 4306, 4334 },
                new int[2]{ 4306, 4335 },
                new int[2]{ 4306, 4336 },
                new int[2]{ 4307, 4305 },
                new int[2]{ 4307, 4306 },
                new int[2]{ 4307, 4309 },
                new int[2]{ 4307, 4310 },
                new int[2]{ 4307, 4311 },
                new int[2]{ 4307, 4313 },
                new int[2]{ 4307, 4314 },
                new int[2]{ 4307, 4315 },
                new int[2]{ 4307, 4316 },
                new int[2]{ 4307, 4318 },
                new int[2]{ 4307, 4319 },
                new int[2]{ 4307, 4320 },
                new int[2]{ 4307, 4321 },
                new int[2]{ 4307, 4322 },
                new int[2]{ 4307, 4324 },
                new int[2]{ 4307, 4325 },
                new int[2]{ 4307, 4326 },
                new int[2]{ 4307, 4327 },
                new int[2]{ 4307, 4328 },
                new int[2]{ 4307, 4329 },
                new int[2]{ 4307, 4330 },
                new int[2]{ 4307, 4331 },
                new int[2]{ 4307, 4332 },
                new int[2]{ 4307, 4333 },
                new int[2]{ 4307, 4334 },
                new int[2]{ 4307, 4335 },
                new int[2]{ 4307, 4336 },
                new int[2]{ 4309, 4305 },
                new int[2]{ 4309, 4306 },
                new int[2]{ 4309, 4307 },
                new int[2]{ 4309, 4310 },
                new int[2]{ 4309, 4311 },
                new int[2]{ 4309, 4313 },
                new int[2]{ 4309, 4314 },
                new int[2]{ 4309, 4315 },
                new int[2]{ 4309, 4316 },
                new int[2]{ 4309, 4318 },
                new int[2]{ 4309, 4319 },
                new int[2]{ 4309, 4320 },
                new int[2]{ 4309, 4321 },
                new int[2]{ 4309, 4322 },
                new int[2]{ 4309, 4324 },
                new int[2]{ 4309, 4325 },
                new int[2]{ 4309, 4326 },
                new int[2]{ 4309, 4327 },
                new int[2]{ 4309, 4328 },
                new int[2]{ 4309, 4329 },
                new int[2]{ 4309, 4330 },
                new int[2]{ 4309, 4331 },
                new int[2]{ 4309, 4332 },
                new int[2]{ 4309, 4333 },
                new int[2]{ 4309, 4334 },
                new int[2]{ 4309, 4335 },
                new int[2]{ 4309, 4336 },
                new int[2]{ 4310, 4305 },
                new int[2]{ 4310, 4306 },
                new int[2]{ 4310, 4307 },
                new int[2]{ 4310, 4309 },
                new int[2]{ 4310, 4311 },
                new int[2]{ 4310, 4313 },
                new int[2]{ 4310, 4314 },
                new int[2]{ 4310, 4315 },
                new int[2]{ 4310, 4316 },
                new int[2]{ 4310, 4318 },
                new int[2]{ 4310, 4319 },
                new int[2]{ 4310, 4320 },
                new int[2]{ 4310, 4321 },
                new int[2]{ 4310, 4322 },
                new int[2]{ 4310, 4324 },
                new int[2]{ 4310, 4325 },
                new int[2]{ 4310, 4326 },
                new int[2]{ 4310, 4327 },
                new int[2]{ 4310, 4328 },
                new int[2]{ 4310, 4329 },
                new int[2]{ 4310, 4330 },
                new int[2]{ 4310, 4331 },
                new int[2]{ 4310, 4332 },
                new int[2]{ 4310, 4333 },
                new int[2]{ 4310, 4334 },
                new int[2]{ 4310, 4335 },
                new int[2]{ 4310, 4336 },
                new int[2]{ 4311, 4305 },
                new int[2]{ 4311, 4306 },
                new int[2]{ 4311, 4307 },
                new int[2]{ 4311, 4309 },
                new int[2]{ 4311, 4310 },
                new int[2]{ 4311, 4313 },
                new int[2]{ 4311, 4314 },
                new int[2]{ 4311, 4315 },
                new int[2]{ 4311, 4316 },
                new int[2]{ 4311, 4318 },
                new int[2]{ 4311, 4319 },
                new int[2]{ 4311, 4320 },
                new int[2]{ 4311, 4321 },
                new int[2]{ 4311, 4322 },
                new int[2]{ 4311, 4324 },
                new int[2]{ 4311, 4325 },
                new int[2]{ 4311, 4326 },
                new int[2]{ 4311, 4327 },
                new int[2]{ 4311, 4328 },
                new int[2]{ 4311, 4329 },
                new int[2]{ 4311, 4330 },
                new int[2]{ 4311, 4331 },
                new int[2]{ 4311, 4332 },
                new int[2]{ 4311, 4333 },
                new int[2]{ 4311, 4334 },
                new int[2]{ 4311, 4335 },
                new int[2]{ 4311, 4336 },
                new int[2]{ 4313, 4305 },
                new int[2]{ 4313, 4306 },
                new int[2]{ 4313, 4307 },
                new int[2]{ 4313, 4309 },
                new int[2]{ 4313, 4310 },
                new int[2]{ 4313, 4311 },
                new int[2]{ 4313, 4314 },
                new int[2]{ 4313, 4315 },
                new int[2]{ 4313, 4316 },
                new int[2]{ 4313, 4318 },
                new int[2]{ 4313, 4319 },
                new int[2]{ 4313, 4320 },
                new int[2]{ 4313, 4321 },
                new int[2]{ 4313, 4322 },
                new int[2]{ 4313, 4324 },
                new int[2]{ 4313, 4325 },
                new int[2]{ 4313, 4326 },
                new int[2]{ 4313, 4327 },
                new int[2]{ 4313, 4328 },
                new int[2]{ 4313, 4329 },
                new int[2]{ 4313, 4330 },
                new int[2]{ 4313, 4331 },
                new int[2]{ 4313, 4332 },
                new int[2]{ 4313, 4333 },
                new int[2]{ 4313, 4334 },
                new int[2]{ 4313, 4335 },
                new int[2]{ 4313, 4336 },
                new int[2]{ 4314, 4305 },
                new int[2]{ 4314, 4306 },
                new int[2]{ 4314, 4307 },
                new int[2]{ 4314, 4309 },
                new int[2]{ 4314, 4310 },
                new int[2]{ 4314, 4311 },
                new int[2]{ 4314, 4313 },
                new int[2]{ 4314, 4315 },
                new int[2]{ 4314, 4316 },
                new int[2]{ 4314, 4318 },
                new int[2]{ 4314, 4319 },
                new int[2]{ 4314, 4320 },
                new int[2]{ 4314, 4321 },
                new int[2]{ 4314, 4322 },
                new int[2]{ 4314, 4324 },
                new int[2]{ 4314, 4325 },
                new int[2]{ 4314, 4326 },
                new int[2]{ 4314, 4327 },
                new int[2]{ 4314, 4328 },
                new int[2]{ 4314, 4329 },
                new int[2]{ 4314, 4330 },
                new int[2]{ 4314, 4331 },
                new int[2]{ 4314, 4332 },
                new int[2]{ 4314, 4333 },
                new int[2]{ 4314, 4334 },
                new int[2]{ 4314, 4335 },
                new int[2]{ 4314, 4336 },
                new int[2]{ 4315, 4305 },
                new int[2]{ 4315, 4306 },
                new int[2]{ 4315, 4307 },
                new int[2]{ 4315, 4309 },
                new int[2]{ 4315, 4310 },
                new int[2]{ 4315, 4311 },
                new int[2]{ 4315, 4313 },
                new int[2]{ 4315, 4314 },
                new int[2]{ 4315, 4316 },
                new int[2]{ 4315, 4318 },
                new int[2]{ 4315, 4319 },
                new int[2]{ 4315, 4320 },
                new int[2]{ 4315, 4321 },
                new int[2]{ 4315, 4322 },
                new int[2]{ 4315, 4324 },
                new int[2]{ 4315, 4325 },
                new int[2]{ 4315, 4326 },
                new int[2]{ 4315, 4327 },
                new int[2]{ 4315, 4328 },
                new int[2]{ 4315, 4329 },
                new int[2]{ 4315, 4330 },
                new int[2]{ 4315, 4331 },
                new int[2]{ 4315, 4332 },
                new int[2]{ 4315, 4333 },
                new int[2]{ 4315, 4334 },
                new int[2]{ 4315, 4335 },
                new int[2]{ 4315, 4336 },
                new int[2]{ 4316, 4305 },
                new int[2]{ 4316, 4306 },
                new int[2]{ 4316, 4307 },
                new int[2]{ 4316, 4309 },
                new int[2]{ 4316, 4310 },
                new int[2]{ 4316, 4311 },
                new int[2]{ 4316, 4313 },
                new int[2]{ 4316, 4314 },
                new int[2]{ 4316, 4315 },
                new int[2]{ 4316, 4318 },
                new int[2]{ 4316, 4319 },
                new int[2]{ 4316, 4320 },
                new int[2]{ 4316, 4321 },
                new int[2]{ 4316, 4322 },
                new int[2]{ 4316, 4324 },
                new int[2]{ 4316, 4325 },
                new int[2]{ 4316, 4326 },
                new int[2]{ 4316, 4327 },
                new int[2]{ 4316, 4328 },
                new int[2]{ 4316, 4329 },
                new int[2]{ 4316, 4330 },
                new int[2]{ 4316, 4331 },
                new int[2]{ 4316, 4332 },
                new int[2]{ 4316, 4333 },
                new int[2]{ 4316, 4334 },
                new int[2]{ 4316, 4335 },
                new int[2]{ 4316, 4336 },
                new int[2]{ 4318, 4305 },
                new int[2]{ 4318, 4306 },
                new int[2]{ 4318, 4307 },
                new int[2]{ 4318, 4309 },
                new int[2]{ 4318, 4310 },
                new int[2]{ 4318, 4311 },
                new int[2]{ 4318, 4313 },
                new int[2]{ 4318, 4314 },
                new int[2]{ 4318, 4315 },
                new int[2]{ 4318, 4316 },
                new int[2]{ 4318, 4319 },
                new int[2]{ 4318, 4320 },
                new int[2]{ 4318, 4321 },
                new int[2]{ 4318, 4322 },
                new int[2]{ 4318, 4324 },
                new int[2]{ 4318, 4325 },
                new int[2]{ 4318, 4326 },
                new int[2]{ 4318, 4327 },
                new int[2]{ 4318, 4328 },
                new int[2]{ 4318, 4329 },
                new int[2]{ 4318, 4330 },
                new int[2]{ 4318, 4331 },
                new int[2]{ 4318, 4332 },
                new int[2]{ 4318, 4333 },
                new int[2]{ 4318, 4334 },
                new int[2]{ 4318, 4335 },
                new int[2]{ 4318, 4336 },
                new int[2]{ 4319, 4305 },
                new int[2]{ 4319, 4306 },
                new int[2]{ 4319, 4307 },
                new int[2]{ 4319, 4309 },
                new int[2]{ 4319, 4310 },
                new int[2]{ 4319, 4311 },
                new int[2]{ 4319, 4313 },
                new int[2]{ 4319, 4314 },
                new int[2]{ 4319, 4315 },
                new int[2]{ 4319, 4316 },
                new int[2]{ 4319, 4318 },
                new int[2]{ 4319, 4320 },
                new int[2]{ 4319, 4321 },
                new int[2]{ 4319, 4322 },
                new int[2]{ 4319, 4324 },
                new int[2]{ 4319, 4325 },
                new int[2]{ 4319, 4326 },
                new int[2]{ 4319, 4327 },
                new int[2]{ 4319, 4328 },
                new int[2]{ 4319, 4329 },
                new int[2]{ 4319, 4330 },
                new int[2]{ 4319, 4331 },
                new int[2]{ 4319, 4332 },
                new int[2]{ 4319, 4333 },
                new int[2]{ 4319, 4334 },
                new int[2]{ 4319, 4335 },
                new int[2]{ 4319, 4336 },
                new int[2]{ 4320, 4305 },
                new int[2]{ 4320, 4306 },
                new int[2]{ 4320, 4307 },
                new int[2]{ 4320, 4309 },
                new int[2]{ 4320, 4310 },
                new int[2]{ 4320, 4311 },
                new int[2]{ 4320, 4313 },
                new int[2]{ 4320, 4314 },
                new int[2]{ 4320, 4315 },
                new int[2]{ 4320, 4316 },
                new int[2]{ 4320, 4318 },
                new int[2]{ 4320, 4319 },
                new int[2]{ 4320, 4321 },
                new int[2]{ 4320, 4322 },
                new int[2]{ 4320, 4324 },
                new int[2]{ 4320, 4325 },
                new int[2]{ 4320, 4326 },
                new int[2]{ 4320, 4327 },
                new int[2]{ 4320, 4328 },
                new int[2]{ 4320, 4329 },
                new int[2]{ 4320, 4330 },
                new int[2]{ 4320, 4331 },
                new int[2]{ 4320, 4332 },
                new int[2]{ 4320, 4333 },
                new int[2]{ 4320, 4334 },
                new int[2]{ 4320, 4335 },
                new int[2]{ 4320, 4336 },
                new int[2]{ 4321, 4305 },
                new int[2]{ 4321, 4306 },
                new int[2]{ 4321, 4307 },
                new int[2]{ 4321, 4309 },
                new int[2]{ 4321, 4310 },
                new int[2]{ 4321, 4311 },
                new int[2]{ 4321, 4313 },
                new int[2]{ 4321, 4314 },
                new int[2]{ 4321, 4315 },
                new int[2]{ 4321, 4316 },
                new int[2]{ 4321, 4318 },
                new int[2]{ 4321, 4319 },
                new int[2]{ 4321, 4320 },
                new int[2]{ 4321, 4322 },
                new int[2]{ 4321, 4324 },
                new int[2]{ 4321, 4325 },
                new int[2]{ 4321, 4326 },
                new int[2]{ 4321, 4327 },
                new int[2]{ 4321, 4328 },
                new int[2]{ 4321, 4329 },
                new int[2]{ 4321, 4330 },
                new int[2]{ 4321, 4331 },
                new int[2]{ 4321, 4332 },
                new int[2]{ 4321, 4333 },
                new int[2]{ 4321, 4334 },
                new int[2]{ 4321, 4335 },
                new int[2]{ 4321, 4336 },
                new int[2]{ 4322, 4305 },
                new int[2]{ 4322, 4306 },
                new int[2]{ 4322, 4307 },
                new int[2]{ 4322, 4309 },
                new int[2]{ 4322, 4310 },
                new int[2]{ 4322, 4311 },
                new int[2]{ 4322, 4313 },
                new int[2]{ 4322, 4314 },
                new int[2]{ 4322, 4315 },
                new int[2]{ 4322, 4316 },
                new int[2]{ 4322, 4318 },
                new int[2]{ 4322, 4319 },
                new int[2]{ 4322, 4320 },
                new int[2]{ 4322, 4321 },
                new int[2]{ 4322, 4324 },
                new int[2]{ 4322, 4325 },
                new int[2]{ 4322, 4326 },
                new int[2]{ 4322, 4327 },
                new int[2]{ 4322, 4328 },
                new int[2]{ 4322, 4329 },
                new int[2]{ 4322, 4330 },
                new int[2]{ 4322, 4331 },
                new int[2]{ 4322, 4332 },
                new int[2]{ 4322, 4333 },
                new int[2]{ 4322, 4334 },
                new int[2]{ 4322, 4335 },
                new int[2]{ 4322, 4336 },
                new int[2]{ 4324, 4305 },
                new int[2]{ 4324, 4306 },
                new int[2]{ 4324, 4307 },
                new int[2]{ 4324, 4309 },
                new int[2]{ 4324, 4310 },
                new int[2]{ 4324, 4311 },
                new int[2]{ 4324, 4313 },
                new int[2]{ 4324, 4314 },
                new int[2]{ 4324, 4315 },
                new int[2]{ 4324, 4316 },
                new int[2]{ 4324, 4318 },
                new int[2]{ 4324, 4319 },
                new int[2]{ 4324, 4320 },
                new int[2]{ 4324, 4321 },
                new int[2]{ 4324, 4322 },
                new int[2]{ 4324, 4325 },
                new int[2]{ 4324, 4326 },
                new int[2]{ 4324, 4327 },
                new int[2]{ 4324, 4328 },
                new int[2]{ 4324, 4329 },
                new int[2]{ 4324, 4330 },
                new int[2]{ 4324, 4331 },
                new int[2]{ 4324, 4332 },
                new int[2]{ 4324, 4333 },
                new int[2]{ 4324, 4334 },
                new int[2]{ 4324, 4335 },
                new int[2]{ 4324, 4336 },
                new int[2]{ 4325, 4305 },
                new int[2]{ 4325, 4306 },
                new int[2]{ 4325, 4307 },
                new int[2]{ 4325, 4309 },
                new int[2]{ 4325, 4310 },
                new int[2]{ 4325, 4311 },
                new int[2]{ 4325, 4313 },
                new int[2]{ 4325, 4314 },
                new int[2]{ 4325, 4315 },
                new int[2]{ 4325, 4316 },
                new int[2]{ 4325, 4318 },
                new int[2]{ 4325, 4319 },
                new int[2]{ 4325, 4320 },
                new int[2]{ 4325, 4321 },
                new int[2]{ 4325, 4322 },
                new int[2]{ 4325, 4324 },
                new int[2]{ 4325, 4326 },
                new int[2]{ 4325, 4327 },
                new int[2]{ 4325, 4328 },
                new int[2]{ 4325, 4329 },
                new int[2]{ 4325, 4330 },
                new int[2]{ 4325, 4331 },
                new int[2]{ 4325, 4332 },
                new int[2]{ 4325, 4333 },
                new int[2]{ 4325, 4334 },
                new int[2]{ 4325, 4335 },
                new int[2]{ 4325, 4336 },
                new int[2]{ 4326, 4305 },
                new int[2]{ 4326, 4306 },
                new int[2]{ 4326, 4307 },
                new int[2]{ 4326, 4309 },
                new int[2]{ 4326, 4310 },
                new int[2]{ 4326, 4311 },
                new int[2]{ 4326, 4313 },
                new int[2]{ 4326, 4314 },
                new int[2]{ 4326, 4315 },
                new int[2]{ 4326, 4316 },
                new int[2]{ 4326, 4318 },
                new int[2]{ 4326, 4319 },
                new int[2]{ 4326, 4320 },
                new int[2]{ 4326, 4321 },
                new int[2]{ 4326, 4322 },
                new int[2]{ 4326, 4324 },
                new int[2]{ 4326, 4325 },
                new int[2]{ 4326, 4327 },
                new int[2]{ 4326, 4328 },
                new int[2]{ 4326, 4329 },
                new int[2]{ 4326, 4330 },
                new int[2]{ 4326, 4331 },
                new int[2]{ 4326, 4332 },
                new int[2]{ 4326, 4333 },
                new int[2]{ 4326, 4334 },
                new int[2]{ 4326, 4335 },
                new int[2]{ 4326, 4336 },
                new int[2]{ 4327, 4305 },
                new int[2]{ 4327, 4306 },
                new int[2]{ 4327, 4307 },
                new int[2]{ 4327, 4309 },
                new int[2]{ 4327, 4310 },
                new int[2]{ 4327, 4311 },
                new int[2]{ 4327, 4313 },
                new int[2]{ 4327, 4314 },
                new int[2]{ 4327, 4315 },
                new int[2]{ 4327, 4316 },
                new int[2]{ 4327, 4318 },
                new int[2]{ 4327, 4319 },
                new int[2]{ 4327, 4320 },
                new int[2]{ 4327, 4321 },
                new int[2]{ 4327, 4322 },
                new int[2]{ 4327, 4324 },
                new int[2]{ 4327, 4325 },
                new int[2]{ 4327, 4326 },
                new int[2]{ 4327, 4328 },
                new int[2]{ 4327, 4329 },
                new int[2]{ 4327, 4330 },
                new int[2]{ 4327, 4331 },
                new int[2]{ 4327, 4332 },
                new int[2]{ 4327, 4333 },
                new int[2]{ 4327, 4334 },
                new int[2]{ 4327, 4335 },
                new int[2]{ 4327, 4336 },
                new int[2]{ 4328, 4305 },
                new int[2]{ 4328, 4306 },
                new int[2]{ 4328, 4307 },
                new int[2]{ 4328, 4309 },
                new int[2]{ 4328, 4310 },
                new int[2]{ 4328, 4311 },
                new int[2]{ 4328, 4313 },
                new int[2]{ 4328, 4314 },
                new int[2]{ 4328, 4315 },
                new int[2]{ 4328, 4316 },
                new int[2]{ 4328, 4318 },
                new int[2]{ 4328, 4319 },
                new int[2]{ 4328, 4320 },
                new int[2]{ 4328, 4321 },
                new int[2]{ 4328, 4322 },
                new int[2]{ 4328, 4324 },
                new int[2]{ 4328, 4325 },
                new int[2]{ 4328, 4326 },
                new int[2]{ 4328, 4327 },
                new int[2]{ 4328, 4329 },
                new int[2]{ 4328, 4330 },
                new int[2]{ 4328, 4331 },
                new int[2]{ 4328, 4332 },
                new int[2]{ 4328, 4333 },
                new int[2]{ 4328, 4334 },
                new int[2]{ 4328, 4335 },
                new int[2]{ 4328, 4336 },
                new int[2]{ 4329, 4305 },
                new int[2]{ 4329, 4306 },
                new int[2]{ 4329, 4307 },
                new int[2]{ 4329, 4309 },
                new int[2]{ 4329, 4310 },
                new int[2]{ 4329, 4311 },
                new int[2]{ 4329, 4313 },
                new int[2]{ 4329, 4314 },
                new int[2]{ 4329, 4315 },
                new int[2]{ 4329, 4316 },
                new int[2]{ 4329, 4318 },
                new int[2]{ 4329, 4319 },
                new int[2]{ 4329, 4320 },
                new int[2]{ 4329, 4321 },
                new int[2]{ 4329, 4322 },
                new int[2]{ 4329, 4324 },
                new int[2]{ 4329, 4325 },
                new int[2]{ 4329, 4326 },
                new int[2]{ 4329, 4327 },
                new int[2]{ 4329, 4328 },
                new int[2]{ 4329, 4330 },
                new int[2]{ 4329, 4331 },
                new int[2]{ 4329, 4332 },
                new int[2]{ 4329, 4333 },
                new int[2]{ 4329, 4334 },
                new int[2]{ 4329, 4335 },
                new int[2]{ 4329, 4336 },
                new int[2]{ 4330, 4305 },
                new int[2]{ 4330, 4306 },
                new int[2]{ 4330, 4307 },
                new int[2]{ 4330, 4309 },
                new int[2]{ 4330, 4310 },
                new int[2]{ 4330, 4311 },
                new int[2]{ 4330, 4313 },
                new int[2]{ 4330, 4314 },
                new int[2]{ 4330, 4315 },
                new int[2]{ 4330, 4316 },
                new int[2]{ 4330, 4318 },
                new int[2]{ 4330, 4319 },
                new int[2]{ 4330, 4320 },
                new int[2]{ 4330, 4321 },
                new int[2]{ 4330, 4322 },
                new int[2]{ 4330, 4324 },
                new int[2]{ 4330, 4325 },
                new int[2]{ 4330, 4326 },
                new int[2]{ 4330, 4327 },
                new int[2]{ 4330, 4328 },
                new int[2]{ 4330, 4329 },
                new int[2]{ 4330, 4331 },
                new int[2]{ 4330, 4332 },
                new int[2]{ 4330, 4333 },
                new int[2]{ 4330, 4334 },
                new int[2]{ 4330, 4335 },
                new int[2]{ 4330, 4336 },
                new int[2]{ 4331, 4305 },
                new int[2]{ 4331, 4306 },
                new int[2]{ 4331, 4307 },
                new int[2]{ 4331, 4309 },
                new int[2]{ 4331, 4310 },
                new int[2]{ 4331, 4311 },
                new int[2]{ 4331, 4313 },
                new int[2]{ 4331, 4314 },
                new int[2]{ 4331, 4315 },
                new int[2]{ 4331, 4316 },
                new int[2]{ 4331, 4318 },
                new int[2]{ 4331, 4319 },
                new int[2]{ 4331, 4320 },
                new int[2]{ 4331, 4321 },
                new int[2]{ 4331, 4322 },
                new int[2]{ 4331, 4324 },
                new int[2]{ 4331, 4325 },
                new int[2]{ 4331, 4326 },
                new int[2]{ 4331, 4327 },
                new int[2]{ 4331, 4328 },
                new int[2]{ 4331, 4329 },
                new int[2]{ 4331, 4330 },
                new int[2]{ 4331, 4332 },
                new int[2]{ 4331, 4333 },
                new int[2]{ 4331, 4334 },
                new int[2]{ 4331, 4335 },
                new int[2]{ 4331, 4336 },
                new int[2]{ 4332, 4305 },
                new int[2]{ 4332, 4306 },
                new int[2]{ 4332, 4307 },
                new int[2]{ 4332, 4309 },
                new int[2]{ 4332, 4310 },
                new int[2]{ 4332, 4311 },
                new int[2]{ 4332, 4313 },
                new int[2]{ 4332, 4314 },
                new int[2]{ 4332, 4315 },
                new int[2]{ 4332, 4316 },
                new int[2]{ 4332, 4318 },
                new int[2]{ 4332, 4319 },
                new int[2]{ 4332, 4320 },
                new int[2]{ 4332, 4321 },
                new int[2]{ 4332, 4322 },
                new int[2]{ 4332, 4324 },
                new int[2]{ 4332, 4325 },
                new int[2]{ 4332, 4326 },
                new int[2]{ 4332, 4327 },
                new int[2]{ 4332, 4328 },
                new int[2]{ 4332, 4329 },
                new int[2]{ 4332, 4330 },
                new int[2]{ 4332, 4331 },
                new int[2]{ 4332, 4332 },
                new int[2]{ 4332, 4333 },
                new int[2]{ 4332, 4334 },
                new int[2]{ 4332, 4335 },
                new int[2]{ 4332, 4336 },
                new int[2]{ 4333, 4305 },
                new int[2]{ 4333, 4306 },
                new int[2]{ 4333, 4307 },
                new int[2]{ 4333, 4309 },
                new int[2]{ 4333, 4310 },
                new int[2]{ 4333, 4311 },
                new int[2]{ 4333, 4313 },
                new int[2]{ 4333, 4314 },
                new int[2]{ 4333, 4315 },
                new int[2]{ 4333, 4316 },
                new int[2]{ 4333, 4318 },
                new int[2]{ 4333, 4319 },
                new int[2]{ 4333, 4320 },
                new int[2]{ 4333, 4321 },
                new int[2]{ 4333, 4322 },
                new int[2]{ 4333, 4324 },
                new int[2]{ 4333, 4325 },
                new int[2]{ 4333, 4326 },
                new int[2]{ 4333, 4327 },
                new int[2]{ 4333, 4328 },
                new int[2]{ 4333, 4329 },
                new int[2]{ 4333, 4330 },
                new int[2]{ 4333, 4331 },
                new int[2]{ 4333, 4332 },
                new int[2]{ 4333, 4334 },
                new int[2]{ 4333, 4335 },
                new int[2]{ 4333, 4336 },
                new int[2]{ 4334, 4305 },
                new int[2]{ 4334, 4306 },
                new int[2]{ 4334, 4307 },
                new int[2]{ 4334, 4309 },
                new int[2]{ 4334, 4310 },
                new int[2]{ 4334, 4311 },
                new int[2]{ 4334, 4313 },
                new int[2]{ 4334, 4314 },
                new int[2]{ 4334, 4315 },
                new int[2]{ 4334, 4316 },
                new int[2]{ 4334, 4318 },
                new int[2]{ 4334, 4319 },
                new int[2]{ 4334, 4320 },
                new int[2]{ 4334, 4321 },
                new int[2]{ 4334, 4322 },
                new int[2]{ 4334, 4324 },
                new int[2]{ 4334, 4325 },
                new int[2]{ 4334, 4326 },
                new int[2]{ 4334, 4327 },
                new int[2]{ 4334, 4328 },
                new int[2]{ 4334, 4329 },
                new int[2]{ 4334, 4330 },
                new int[2]{ 4334, 4331 },
                new int[2]{ 4334, 4332 },
                new int[2]{ 4334, 4333 },
                new int[2]{ 4334, 4335 },
                new int[2]{ 4334, 4336 },
                new int[2]{ 4335, 4305 },
                new int[2]{ 4335, 4306 },
                new int[2]{ 4335, 4307 },
                new int[2]{ 4335, 4309 },
                new int[2]{ 4335, 4310 },
                new int[2]{ 4335, 4311 },
                new int[2]{ 4335, 4313 },
                new int[2]{ 4335, 4314 },
                new int[2]{ 4335, 4315 },
                new int[2]{ 4335, 4316 },
                new int[2]{ 4335, 4318 },
                new int[2]{ 4335, 4319 },
                new int[2]{ 4335, 4320 },
                new int[2]{ 4335, 4321 },
                new int[2]{ 4335, 4322 },
                new int[2]{ 4335, 4324 },
                new int[2]{ 4335, 4325 },
                new int[2]{ 4335, 4326 },
                new int[2]{ 4335, 4327 },
                new int[2]{ 4335, 4328 },
                new int[2]{ 4335, 4329 },
                new int[2]{ 4335, 4330 },
                new int[2]{ 4335, 4331 },
                new int[2]{ 4335, 4332 },
                new int[2]{ 4335, 4333 },
                new int[2]{ 4335, 4334 },
                new int[2]{ 4335, 4336 },
                new int[2]{ 4336, 4305 },
                new int[2]{ 4336, 4306 },
                new int[2]{ 4336, 4307 },
                new int[2]{ 4336, 4309 },
                new int[2]{ 4336, 4310 },
                new int[2]{ 4336, 4311 },
                new int[2]{ 4336, 4313 },
                new int[2]{ 4336, 4314 },
                new int[2]{ 4336, 4315 },
                new int[2]{ 4336, 4316 },
                new int[2]{ 4336, 4318 },
                new int[2]{ 4336, 4319 },
                new int[2]{ 4336, 4320 },
                new int[2]{ 4336, 4321 },
                new int[2]{ 4336, 4322 },
                new int[2]{ 4336, 4324 },
                new int[2]{ 4336, 4325 },
                new int[2]{ 4336, 4326 },
                new int[2]{ 4336, 4327 },
                new int[2]{ 4336, 4328 },
                new int[2]{ 4336, 4329 },
                new int[2]{ 4336, 4330 },
                new int[2]{ 4336, 4331 },
                new int[2]{ 4336, 4332 },
                new int[2]{ 4336, 4333 },
                new int[2]{ 4336, 4334 },
                new int[2]{ 4336, 4335 }
            };

            //wordApp = FindAndReplace(wordApp, ((char)varData[i][0]).ToString() + ((char)varData[i][1]).ToString(), ((char)varData[i][0]).ToString() + ((char)varData[i][1]).ToString() + ((char)31).ToString());
            for (int i = 0; i < 756; i++)
            {
                wordApp = FindAndReplace(wordApp, ((char)varData[i][0]).ToString() + ((char)varData[i][1]).ToString(), ((char)varData[i][0]).ToString() + ((char)varData[i][1]).ToString() + ((char)31).ToString());
            }
            Console.WriteLine("end_cons");

            return wordApp;
        }

        public Application HYPwovels(Application wordApp)
        {
            Console.WriteLine("st_vow");

            wordApp = FindAndReplace(wordApp, "([აეიოუ])", @"\1" + ((char)31).ToString());

            Console.WriteLine("end_vow");
            return wordApp;
        }

        public Application HYPcleanfirst(Application wordApp)
        {
            Console.WriteLine("st_cleanFirst");

            wordApp = FindAndReplace(wordApp, @"([\ \(\„\”])([ა-ჰ])" + ((char)31).ToString(), @"\1\2");

            Console.WriteLine("end_cleanFirst");
            return wordApp;
        }

        public Application HYPcleanlast(Application wordApp)
        {
            Console.WriteLine("st_cllast");

            wordApp = FindAndReplace(wordApp, @"([ა-ჰ])" + ((char)31).ToString() + @"([\ \.\,\!\?\)\-\;\:\“^13])", @"\1\2");
            wordApp = FindAndReplace(wordApp, ((char)31).ToString() + @"([ა-ჰ])" + @"([\ \.\,\!\?\)\-\;\:\“^13])", @"\1\2");

            Console.WriteLine("end_cllast");

            return wordApp;
        }

        public Application HYPcleanlastconpunct(Application wordApp)
        {
            Console.WriteLine("st_cllastcomp");

            int[][] varData = new int[58][]
            {
                new int[2]{ 4305, 4321 },
                new int[2]{ 4314, 4321 },
                new int[2]{ 4309, 4321 },
                new int[2]{ 4305, 4311 },
                new int[2]{ 4316, 4321 },
                new int[2]{ 4320, 4321 },
                new int[2]{ 4322, 4321 },
                new int[2]{ 4309, 4311 },
                new int[2]{ 4315, 4321 },
                new int[2]{ 4320, 4311 },
                new int[2]{ 4311, 4321 },
                new int[2]{ 4307, 4321 },
                new int[2]{ 4313, 4321 },
                new int[2]{ 4316, 4311 },
                new int[2]{ 4306, 4321 },
                new int[2]{ 4310, 4321 },
                new int[2]{ 4314, 4311 },
                new int[2]{ 4334, 4321 },
                new int[2]{ 4324, 4321 },
                new int[2]{ 4315, 4311 },
                new int[2]{ 4305, 4320 },
                new int[2]{ 4325, 4321 },
                new int[2]{ 4321, 4322 },
                new int[2]{ 4328, 4321 },
                new int[2]{ 4316, 4307 },
                new int[2]{ 4316, 4322 },
                new int[2]{ 4320, 4307 },
                new int[2]{ 4316, 4306 },
                new int[2]{ 4330, 4321 },
                new int[2]{ 4329, 4321 },
                new int[2]{ 4318, 4321 },
                new int[2]{ 4320, 4322 },
                new int[2]{ 4335, 4321 },
                new int[2]{ 4319, 4321 },
                new int[2]{ 4316, 4330 },
                new int[2]{ 4315, 4330 },
                new int[2]{ 4314, 4307 },
                new int[2]{ 4321, 4311 },
                new int[2]{ 4322, 4320 },
                new int[2]{ 4324, 4311 },
                new int[2]{ 4309, 4316 },
                new int[2]{ 4320, 4330 },
                new int[2]{ 4326, 4321 },
                new int[2]{ 4305, 4316 },
                new int[2]{ 4320, 4313 },
                new int[2]{ 4315, 4307 },
                new int[2]{ 4314, 4315 },
                new int[2]{ 4320, 4316 },
                new int[2]{ 4305, 4314 },
                new int[2]{ 4309, 4314 },
                new int[2]{ 4331, 4321 },
                new int[2]{ 4333, 4321 },
                new int[2]{ 4321, 4313 },
                new int[2]{ 4311, 4334 },
                new int[2]{ 4307, 4320 }, // დ და რ
                new int[2]{ 4311, 4316 },
                new int[2]{ 4327, 4321 },
                new int[2]{ 4314, 4316 }
            };

            for (int i = 0; i < 57; i++)
            {
                wordApp = FindAndReplace(wordApp, $"{((char)31).ToString()}({((char)varData[i][0]).ToString()}{((char)varData[i][1]).ToString()})" + @"([\ \.\,\!\?\)\-\;\:\“^13])", @"\1\2");
            }

            Console.WriteLine("end_cllastcomp");

            return wordApp;
        }

        public Application FindAndReplace(Application wordApp, object toFindText, object replaceWithText)
        {
            object matchCase = false;

            object matchwholeWord = true;

            object matchwildCards = true;

            object matchSoundLike = false;

            object nmatchAllforms = false;

            object forward = true;

            object format = false;

            object matchKashida = false;

            object matchDiactitics = false;

            object matchAlefHamza = false;

            object matchControl = false;

            object read_only = false;

            object visible = true;

            object replace = WdReplace.wdReplaceAll;

            object wrap = 1;

            try
            {
                wordApp.Selection.Find.Execute(ref toFindText, ref matchCase,
                                                ref matchwholeWord, ref matchwildCards, ref matchSoundLike,
                                                ref nmatchAllforms, ref forward,
                                                ref wrap, ref format, ref replaceWithText,
                                                ref replace, ref matchKashida,
                                                ref matchDiactitics, ref matchAlefHamza,
                                                ref matchControl);
                return wordApp;
            }
            catch (Exception e)
            {
                throw new Exception($"Err: {e.Message}");
            }
        }
    }
}
