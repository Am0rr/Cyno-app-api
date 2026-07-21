export type LitterStatus = 'Draft' | 'Submitted' | 'Approved' | 'Published';

export interface GetLittersRequest {
  status?: LitterStatus;
  pageNumber: number;
  pageSize: number;
}

export interface LitterResponse {
  id: string;
  breederId: string;
  status: LitterStatus;
  createdAt: string;
}

export interface PagedLitterResponse {
  items: LitterResponse[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
}

export interface PublishResponse {
  litterId: string;
  status: LitterStatus;
}
